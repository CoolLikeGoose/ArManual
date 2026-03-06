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
        [Header("Dependencies")]
        [SerializeField] private APILoader apiLoader;
        [SerializeField] private ArUcoDetector arUcoDetector;
        [SerializeField] private XROrigin origin;
        [SerializeField] private ARAnchorManager arAnchorManager;
        [SerializeField] private GameObject markerControllerPrefab;
    
        private Dictionary<int, MarkerController> markerControllers = new();
        private Dictionary<int, TrackPointModel> trackPoints = new();
        private Dictionary<int, List<InteractionPointModel>> interactionPoints = new();
    
        // Avoiding multiple IP creation with the same ID
        private Queue<MarkerDetectionResult> pendingMarkers = new();
        // Avoiding pending markers creation after scenario change
        private int scenarioToken = 0;
    
        private bool isInitialized = false;

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
            DebugController.Log(this, "Loading scenario: " + scenarioModel.name);
            isInitialized = false;
            Clear();
        
            List<int> usedArucoIds = new List<int>();

            foreach (var interaction in scenarioModel.Interactions)
            {
                DebugController.Log(this, "Interaction ID: " + interaction.interactionID);
                InteractionPointModel iPoint = apiLoader.LoadIPointByID(interaction.interactionPointID);
                TrackPointModel trackPoint = apiLoader.LoadTrackPointByID(iPoint.trackpointID);

                if (!trackPoints.Values.Contains(trackPoint))
                {
                    trackPoints.Add(trackPoint.arucoID, trackPoint);
                }
            
                // For whitelist
                if (!usedArucoIds.Contains(trackPoint.arucoID))
                {
                    usedArucoIds.Add(trackPoint.arucoID);
                }
            
                // Load interaction points for future creation
                if (!interactionPoints.ContainsKey(trackPoint.arucoID))
                {
                    interactionPoints[trackPoint.arucoID] = new List<InteractionPointModel>();
                }
                interactionPoints[trackPoint.arucoID].Add(iPoint);
            } 
        
            arUcoDetector.SetWhitelist(usedArucoIds);
            isInitialized = true;
            DebugController.Log(this, 
                $"Loaded scenario: {scenarioModel.scenarioID} with {interactionPoints.Count} interactions");
        }

        private void OnMarkersDetected(List<MarkerDetectionResult> markers)
        {
            if (!isInitialized) return;
        
            foreach (var marker in markers)
            {
                if (!markerControllers.ContainsKey(marker.ID))
                {
                    if (pendingMarkers.Contains(marker)) 
                        continue;
                    
                    pendingMarkers.Enqueue(marker);
                    CreateMarkerController(marker);
                }
                else
                {
                    UpdateMarkerController(marker);
                }
            }
        }

        private async void CreateMarkerController(MarkerDetectionResult marker)
        {
            int currentToken = scenarioToken;
            
            float markerSize = trackPoints[marker.ID].sizeCm / 100f;
            if (!arUcoDetector.EstimatePose(marker.ID, markerSize, out Vector3 rotation, out Vector3 translation))
            {
                DebugController.Log(this, "Failed to estimate position for marker: " + marker.ID);
                pendingMarkers.Dequeue();
                return;
            }
            
            Pose worldPose = OpenCvToUnityWorldPose(rotation, translation);
            var result = await arAnchorManager.TryAddAnchorAsync(worldPose);
            
            // Check if a scenario changed while waiting for anchor creation
            if (currentToken != scenarioToken)
            {
                DebugController.Log(this, "Scenario changed while waiting for anchor creation");
                if (result.status.IsSuccess())
                    Destroy(result.value.gameObject);
                return;
            }
            
            if (!result.status.IsSuccess())
            {
                DebugController.Log(this, "Failed to add anchor for marker: " + marker.ID);
                pendingMarkers.Dequeue();
                return;
            }

            ARAnchor anchor = result.value;
        
            GameObject markerControllerObj = Instantiate(markerControllerPrefab, anchor.transform);
            MarkerController markerController = markerControllerObj.GetComponent<MarkerController>();
        
            markerController.Initialize(marker.ID, anchor, interactionPoints[marker.ID], Camera.main);
            
            markerControllers[marker.ID] = markerController;
            
            StatusManager.Instance.UpdateMarker(marker.ID, true, marker.sizeInPixels, translation.z);
            pendingMarkers.Dequeue();
            DebugController.Log(this, 
                "Created marker_" + marker.ID + 
                ", with size: " + trackPoints[marker.ID].sizeCm +
                ", IPs count: " + interactionPoints[marker.ID].Count);
        }

        private void UpdateMarkerController(MarkerDetectionResult marker)
        {
            StatusManager.Instance.UpdateMarker(marker.ID, marker.sizeInPixels);
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
            scenarioToken++;
            
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
            pendingMarkers.Clear();
        }
    }
}
