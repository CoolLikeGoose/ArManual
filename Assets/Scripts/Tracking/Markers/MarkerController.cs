using System.Collections.Generic;
using DebugTools;
using ManualSession;
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
        [SerializeField] private GameObject markerCollapsed;
        [SerializeField] private GameObject debugAnchorPrefab;
        [SerializeField] private FreezeManager freezeManager;

        private ARAnchor anchor;
        private GameObject root;
        private GameObject iPointsHolder;
        private GameObject markerPlaceholder;
        
        private List<InteractionPointController> interactionPoints = new();

        private const float CollapseThreshold = 30f;
        private const float CollapseThresholdMin = 25f; // Prevent flickering
        private bool? isCollapsed;
        
        private const float RecalibrationThreshold = 1f;
        private bool isVisible = true;
        private float lastSeenTime;
        private float lastSeenSize;
        private float closestSeenSize;
        
        private bool isPaused = false;
        private bool isHidden = false;

        private int markerID { get; set; }

        private void OnEnable()
        {
            if (freezeManager != null)
                freezeManager.OnFreeze += OnPause;
        }
        
        private void OnDisable()
        {
            if (freezeManager != null)
                freezeManager.OnFreeze -= OnPause;
        }

        public void SetFreezeManager(FreezeManager freezeManagerRef)
        {
            freezeManager = freezeManagerRef;
            freezeManagerRef.OnFreeze += OnPause;
        }

        public void Initialize(int markerID, ARAnchor anchor, List<InteractionPointModel> iPoints, 
            Camera camera, float sizeInPixels)
        {
            this.markerID = markerID;
            this.anchor = anchor;
            lastSeenSize = sizeInPixels;
            closestSeenSize = sizeInPixels;
        
            root = new GameObject("Marker_" + markerID);
            root.transform.SetParent(anchor.transform, false);

            // Holds collapsed marker
            markerPlaceholder = Instantiate(markerCollapsed, root.transform, false);
            
            // Holds iPoints as children
            iPointsHolder = new GameObject("iPointHolder_" + markerID);
            iPointsHolder.transform.SetParent(root.transform, false);

            foreach (InteractionPointModel interactionPointModel in iPoints)
            {
                GameObject pointObj = Instantiate(iPointPrefab, iPointsHolder.transform);
                InteractionPointController point =  pointObj.GetComponent<InteractionPointController>();
                point.Initialize(interactionPointModel, camera);
                interactionPoints.Add(point);
            }
        
            Instantiate(debugAnchorPrefab, iPointsHolder.transform);
            
            UpdateVisibility();
            lastSeenTime = Time.time;
        }

        public void OnMarkerSeen()
        {
            lastSeenTime = Time.time;
            
            if (!isHidden && !isVisible)
            {
                ToggleVisibility(true);
            }
        }
        
        public bool OnMarkerSeen(float sizeInPixels)
        {
            bool needsRecalibration = (sizeInPixels - closestSeenSize) > RecalibrationThreshold;
            if (needsRecalibration) 
                closestSeenSize = sizeInPixels;
            
            lastSeenSize = sizeInPixels;
            OnMarkerSeen();
            UpdateVisibility();
            
            return needsRecalibration;
        }

        private void UpdateVisibility()
        {
            bool collapse = lastSeenSize < (isCollapsed == false ? CollapseThresholdMin : CollapseThreshold);
            
            if (isCollapsed == collapse) 
                return;

            isCollapsed = collapse;
            iPointsHolder.SetActive(!collapse);
            markerPlaceholder.SetActive(collapse);
        }

        public void UpdatePosition(Pose worldPose)
        {
            // TODO: Create smooth transition
            root.transform.position = worldPose.position;
            root.transform.rotation = worldPose.rotation;
        }

        private void Update()
        {
            if (!isHidden && 
                isVisible && 
                !isPaused && 
                Time.time - lastSeenTime > hideAfter)
            {
                ToggleVisibility(false);
            }
        }
        
        private void OnPause(bool pauseStatus)
        {
            isPaused = pauseStatus;
            if (isPaused)
            {
                iPointsHolder.transform.SetParent(freezeManager.GetHolderTransform(), true);
            }
            else
            {
                iPointsHolder.transform.SetParent(root.transform, true);
                iPointsHolder.transform.localPosition = Vector3.zero;
                iPointsHolder.transform.localRotation = Quaternion.identity;
            }
        }

        private void ToggleVisibility(bool enable)
        {
            isVisible = enable;
            StatusManager.Instance.UpdateMarker(markerID, enable);
            root.SetActive(enable);
        }

        public void TemporarilyToggleVisibility(bool enable)
        {
            if (isHidden == enable) 
                return;
            
            isHidden = enable;
            
            if (isHidden && isVisible) 
                ToggleVisibility(enable);
        }

        public void ShowOnly(int interactionPointID)
        {
            foreach (var iPController in interactionPoints)
            {
                iPController.gameObject.SetActive(iPController.iPointId == interactionPointID);
            }
        }

        public void Cleanup()
        {
            if (root) Destroy(root);
            if (anchor) Destroy(anchor.gameObject);
        }
    }
}
