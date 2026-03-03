using System.Collections.Generic;
using Core;
using Detection;
using UnityEngine;

namespace DebugTools.Visualization
{
    [RequireComponent(typeof(ArUcoDetector))]
    public class DebugOverlay : MonoBehaviour
    {
        [Header("Visualization")] 
        [SerializeField] private bool showBorders = true;
        [SerializeField] private bool showLabels = true;
        [SerializeField] private int labelFontSize = 32;
        
        private ArUcoDetector detector;
        private Material lineMaterial;
        private List<MarkerDetectionResult> currentMarkers = new();
        private List<(Vector2 screenPos, int id)> labels = new();

        private float markerDisplayTimeout = 0.5f;
        private float lastDetectionTime = 0;

        private void Awake()
        {
            detector = GetComponent<ArUcoDetector>();
            CreateLineMaterial();
        }

        private void Start()
        {
            if (!AppController.Instance.enableDebugOverlay)
            {
                detector.OnMarkersDetected -= OnMarkersDetected;

                currentMarkers.Clear();
                labels.Clear();
            }
        }

        private void OnEnable()
        {
            if (detector != null)
            {
                detector.OnMarkersDetected += OnMarkersDetected;
            }
        }

        private void OnDisable()
        {
            if (detector != null)
            {
                detector.OnMarkersDetected -= OnMarkersDetected;
            }
        }

        // Material setup
        private void CreateLineMaterial()
        {
            lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            lineMaterial.SetInt("_ZWrite", 0);
        }

        private void OnMarkersDetected(List<MarkerDetectionResult> markers)
        {
            currentMarkers = new List<MarkerDetectionResult>(markers);
            lastDetectionTime = Time.time;
        }

        private bool isTimeOut()
        {
            if (Time.time - lastDetectionTime > markerDisplayTimeout)
            {
                currentMarkers.Clear();
                labels.Clear();
                return true;
            }

            return false;
        }
        
        void OnRenderObject()
        {
            if (isTimeOut()) return;
            
            if (!showBorders || currentMarkers.Count == 0 || detector == null)
                return;

            var cameraDims = detector.GetCameraDimensions();
            if (cameraDims.x == 0 || cameraDims.y == 0)
                return;
            
            labels.Clear();

            lineMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();

            GL.Begin(GL.LINES);
            GL.Color(Color.green);
            
            foreach (var m in currentMarkers)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 p1 = detector.PixelToNormalized(m.Corners[i]);
                    Vector2 p2 = detector.PixelToNormalized(m.Corners[(i + 1) % 4]);

                    GL.Vertex3(p1.x, p1.y, 0);
                    GL.Vertex3(p2.x, p2.y, 0);
                }

                // Center for text
                if (showLabels)
                {
                    Vector2 center = detector.GetMarkerCenter(m);
                    Vector2 normCenter = detector.PixelToNormalized(center);
                    labels.Add((normCenter, m.ID));   
                }
            }

            GL.End();
            GL.PopMatrix();
        }
    
        void OnGUI()
        {
            if (isTimeOut()) return;
            
            if (!showLabels || labels.Count == 0) 
                return;
    
            GUIStyle st = new GUIStyle(GUI.skin.label)
            {
                normal =
                {
                    textColor = Color.red
                },
                fontSize = labelFontSize,
                alignment = TextAnchor.MiddleCenter
            };

            foreach (var l in labels)
            {
                Vector2 screen = new Vector2(
                    l.screenPos.x * Screen.width,
                    Screen.height - l.screenPos.y * Screen.height
                );

                GUI.Label(new Rect(screen.x - 50, screen.y - 20, 100, 40), l.id.ToString(), st);
            }
        }
    }
}
