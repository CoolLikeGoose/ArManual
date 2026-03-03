using System.Collections.Generic;
using System.Linq;
using DebugTools;
using Detection;
using Models;
using Network;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Tracking.Markers
{
    public class MarkerManager : MonoBehaviour
    {
        [Header("Settings")]
        // [SerializeField] private float ;
        [Header("Debug")]
    

        [Header("Dependencies")]
        [SerializeField] private ArUcoDetector arUcoDetector;
        [SerializeField] private XROrigin origin;
        [SerializeField] private ARAnchorManager arAnchorManager;
        [SerializeField] private GameObject markerControllerPrefab;
    
        private Dictionary<int, MarkerController> markerControllers = new();
        private Dictionary<int, TrackPointModel> trackPoints = new();
        private Dictionary<int, List<InteractionPointModel>> interactionPoints = new();
    
        // Avoiding multiple IP creation with the same ID
        private Queue<MarkerDetectionResult> pendingMarkers = new();
    
        private bool isInitialzed = false;

        private void OnEnable()
        {
            if (arUcoDetector != null)
            {
                arUcoDetector.OnMarkersDetected += OnMarkersDetected;
            }
        }

        private void OnDisable()
        {
            if (arUcoDetector != null)
            {
                arUcoDetector.OnMarkersDetected -= OnMarkersDetected;
            }
        }

        public void LoadScenario(ScenarioModel scenarioModel)
        {
            isInitialzed = false;
            Clear();
        
            List<int> usedArucoIds = new List<int>();

            foreach (var interaction in scenarioModel.Interactions)
            {
                DebugController.Log(this, "Interaction ID: " + interaction.interactionID);
                InteractionPointModel iPoint = APILoader.Instance.LoadIPointByID(interaction.interactionPointID);
                TrackPointModel trackPoint = APILoader.Instance.LoadTrackPointByID(iPoint.trackpointID);

                if (!trackPoints.Values.Contains(trackPoint))
                {
                    trackPoints.Add(trackPoint.arucoID, trackPoint);
                    // Log("Added to trackpoints");
                }
            
                // For whitelist
                if (!usedArucoIds.Contains(trackPoint.arucoID))
                {
                    usedArucoIds.Add(trackPoint.arucoID);
                    // Log("Added to used arucoids");
                }
            
                // Load interaction points for future creation
                if (!interactionPoints.ContainsKey(trackPoint.arucoID))
                {
                    interactionPoints[trackPoint.arucoID] = new List<InteractionPointModel>();
                    // Log("Initialized interaction points for new arucoid");
                }
                interactionPoints[trackPoint.arucoID].Add(iPoint);
                // Log("Added to interaction points");
            } 
        
            arUcoDetector.SetWhitelist(usedArucoIds);
            isInitialzed = true;
            DebugController.Log(this, $"Loaded scenario: {scenarioModel.scenarioID} with {interactionPoints.Count} interactions");
        }

        private void OnMarkersDetected(List<MarkerDetectionResult> markers)
        {
            if (!isInitialzed) return;
        
            foreach (var marker in markers)
            {
                if (!markerControllers.ContainsKey(marker.ID))
                {
                    if (!pendingMarkers.Contains(marker))
                    {
                        pendingMarkers.Enqueue(marker);
                        CreateMarkerController(marker);
                    }
                }
                else
                {
                    UpdateMarkerController(marker);
                }
            }
        }

        private async void CreateMarkerController(MarkerDetectionResult marker)
        {
            float markerSize = trackPoints[marker.ID].sizeCm / 100f;
            if (!arUcoDetector.EstimatePose(marker.ID, markerSize, out Vector3 rotation, out Vector3 translation))
            {
                DebugController.Log(this, "Failed to estimate position for marker: " + marker.ID);
                return;
            }

            Pose worldPose = OpenCvToUnityWorldPose(rotation, translation);
            var result = await arAnchorManager.TryAddAnchorAsync(worldPose);
            if (!result.status.IsSuccess())
            {
                DebugController.Log(this, "Failed to add anchor for marker: " + marker.ID);
                return;
            }

            ARAnchor anchor = result.value;
        
            GameObject markerControllerObj = Instantiate(markerControllerPrefab, anchor.transform);
            MarkerController markerController = markerControllerObj.GetComponent<MarkerController>();
        
            markerController.Initialize(marker.ID, anchor, interactionPoints[marker.ID], Camera.main);
        
            markerControllers[marker.ID] = markerController;
        
            pendingMarkers.Dequeue();
            DebugController.Log(this, 
                "Created marker_" + marker.ID + 
                ", with size: " + trackPoints[marker.ID].sizeCm +
                ", IPs count: " + interactionPoints[marker.ID].Count);
        }

        private void UpdateMarkerController(MarkerDetectionResult marker)
        {
            markerControllers[marker.ID].OnMarkerSeen();
        }

        // private void UpdateMarkerPositions(MarkerDetectionResult marker)
        // {
        //     float markerSize = trackPoints[marker.ID].sizeCm / 100f;
        //     if (!arUcoDetector.EstimatePose(marker.ID, markerSize, out Vector3 rotation, out Vector3 translation))
        //     {
        //         Log("Failed to estimate position for marker: " + marker.ID);
        //         return;
        //     }
        //     
        //     var obj = markerControllers[marker.ID];
        //     obj.transform.SetWorldPose(OpenCvToUnityWorldPose(rotation, translation));
        // }
    
        // ----------------------------Conversions----------------------------
        private Pose OpenCvToUnityWorldPose(Vector3 rotation, Vector3 translation)
        {
            Vector3 cameraTrans = OpenCvTranslationToUnity(translation);   
            Quaternion cameraRot = OpenCvRotationToUnity(rotation);
        
            Transform cam = origin.Camera.transform;
            Vector3 worldPos = cam.TransformPoint(cameraTrans);
            Quaternion worldRot = cam.rotation * cameraRot;
        
            return new Pose(worldPos, worldRot);
        }

        private Vector3 OpenCvTranslationToUnity(Vector3 translation)
        {
            return new Vector3(
                -translation.x,
                translation.y,
                translation.z
            );
        }
    
        private Quaternion OpenCvRotationToUnity(Vector3 rotation)
        {
            Quaternion cvRot = RodriguezToQuaternion(rotation);
        
            Quaternion unityRot = new Quaternion(
                cvRot.x,
                -cvRot.y,
                -cvRot.z,
                cvRot.w
            );
            //TODO: Check if coordinate system correlates
    
            return unityRot;
        }

        private Quaternion RodriguezToQuaternion(Vector3 rotation)
        {
            // in rads
            float angle = rotation.magnitude;
    
            if (angle < 0.0001f)
                return Quaternion.identity;
    
            Vector3 axis = rotation / angle;
    
            // Quaternion from axis-angle
            float halfAngle = angle * 0.5f;
            float sinHalf = Mathf.Sin(halfAngle);
            float cosHalf = Mathf.Cos(halfAngle);
    
            return new Quaternion(
                axis.x * sinHalf,
                axis.y * sinHalf,
                axis.z * sinHalf,
                cosHalf
            );
        }
    
        public void Clear()
        {
            //TODO: Add check for pending markers
        
            foreach (var controller in markerControllers.Values)
            {
                if (controller != null) controller.Cleanup();
            }

            foreach (var interactionPoint in interactionPoints)
            {
                interactionPoint.Value.Clear();
            }
        
            interactionPoints.Clear();
            markerControllers.Clear();
            trackPoints.Clear();
        }
    }
}
