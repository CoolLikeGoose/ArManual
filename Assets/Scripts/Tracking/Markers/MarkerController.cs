using System.Collections.Generic;
using DebugTools;
using Models;
using Tracking.InteractionPoints;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Tracking.Markers
{
    public class MarkerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float hideAfter = 1f;
        // [Header("Debug")]

        [Header("Dependencies")]
        [SerializeField] private GameObject iPointPrefab;
        [SerializeField] private GameObject debugAnchorPrefab;

        private ARAnchor anchor;
        private GameObject root;
        private List<InteractionPointController> interactionPoints = new();
        private bool isVisible = true;
        private float lastSeenTime;
    
        public int MarkerID { get; private set; }
        public ARAnchor Anchor => anchor;

        public void Initialize(int markerID, ARAnchor anchor, List<InteractionPointModel> iPoints, Camera camera)
        {
            MarkerID = markerID;
            this.anchor = anchor;
        
            root = new GameObject("MarkerRoot_" + markerID);
            root.transform.SetParent(anchor.transform, false);

            foreach (InteractionPointModel interactionPointModel in iPoints)
            {
                GameObject pointObj = Instantiate(iPointPrefab, root.transform);
                InteractionPointController point =  pointObj.GetComponent<InteractionPointController>();
                point.Initialize(interactionPointModel, camera);
                interactionPoints.Add(point);
            }
        
            Instantiate(debugAnchorPrefab, root.transform);
        
            lastSeenTime = Time.time;
        }

        public void OnMarkerSeen()
        {
            lastSeenTime = Time.time;

            if (!isVisible)
            {
                isVisible = true;
                StatusManager.Instance.UpdateMarker(MarkerID, true);
                root.SetActive(true);
            }
        }

        private void Update()
        {
            if (isVisible && Time.time - lastSeenTime > hideAfter)
            {
                isVisible = false;
                StatusManager.Instance.UpdateMarker(MarkerID, false);
                root.SetActive(false);
            }
        }

        public void CalibrateAnchorPose(Pose pose)
        {
        
        }

        public void Cleanup()
        {
            if (root != null) Destroy(root);
            if (anchor != null) Destroy(anchor.gameObject);
        }
    }
}
