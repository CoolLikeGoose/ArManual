using TMPro;
using UnityEngine;

namespace DebugTools.Visualization
{
    public class DebugInteractionPoint : MonoBehaviour
    {
        [Header("Settings")]
        [Header("Sphere")]
        [SerializeField] private float sphereScale = 0.05f;
        [SerializeField] private Color sphereColor = Color.cyan;
    
        [Header("Text")]
        [SerializeField] private float textFontSize = 0.5f; 
    
        [Header("Dependencies")]
        [SerializeField] private TextMeshPro label;
        [SerializeField] private MeshRenderer meshRenderer;

        private int iPointID = -1;

        private void Awake()
        {
            if (label != null)
            {
                label.text = $"IP_{iPointID}";
                label.fontSize = textFontSize;
                label.alignment = TextAlignmentOptions.Center;

                label.transform.localPosition = new Vector3(0, sphereScale + .02f, 0);
            }
        
            if (meshRenderer != null)
            {
                meshRenderer.material.color = sphereColor;
            }
        
            transform.localScale = Vector3.one * sphereScale;
        }

        public void Initialize(int iPointIDnew)
        {
            iPointID = iPointIDnew;

            if (label != null)
            {
                label.text =  $"IP_{iPointID}";
            }
        
            gameObject.name = "IP_" + iPointID;
        }
    }
}
