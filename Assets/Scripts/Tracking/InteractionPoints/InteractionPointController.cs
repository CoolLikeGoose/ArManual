using System.Collections;
using Models;
using TMPro;
using UnityEngine;

namespace Tracking.InteractionPoints
{
    public class InteractionPointController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float billBoardSpeed = 10f;
        [SerializeField] private float padding = 0.01f;
        [SerializeField] private float sphereSize = 0.01f;

        [Header("Dependencies")]
        [SerializeField] private Transform quadTransform;
        [SerializeField] private MeshRenderer sphereRenderer;
        [SerializeField] private TextMeshPro labelText;
        [SerializeField] private Collider hitbox;

        public string label => labelText.text;
        public int iPointId { get; private set; }
        public string contentText { get; private set; }
    
        private Camera cam;

        public void Initialize(InteractionPointModel data, Camera cam)
        {
            this.cam = cam;
            iPointId = data.interactionPointID;

            // Position
            transform.localPosition = data.position / 100f;
        
            // Visual
            labelText.text = data.iPointName;
            sphereRenderer.material.color = Color.red;
            contentText = data.content;

            // Frame skip for TMP to adjust text size
            StartCoroutine(ResizeQuad());
        }

        private IEnumerator ResizeQuad()
        {
            yield return null;

            Vector2 textSize = labelText.GetRenderedValues(false);
        
            float totalWidth = sphereSize + textSize.x + padding * 3;
            float totalHeight = quadTransform.localScale.y;
            quadTransform.localScale = new Vector3(totalWidth, totalHeight, 1f);
        
            float quadShift = totalWidth/2 - sphereSize/2 - padding;
            quadTransform.localPosition = new Vector3(quadShift, 0, 0);
        
            // Move text
            float textShift = textSize.x/2 + sphereSize/2 + padding;
            labelText.transform.localPosition = new Vector3(textShift, 0, -0.01f);
        }

        private void Update()
        {
            if (!cam) 
                return;
            
            // Looking toward the camera
            Vector3 dirToCamera = cam.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(-dirToCamera);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * billBoardSpeed);
        }
    }
}
