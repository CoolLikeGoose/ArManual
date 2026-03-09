using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Core;
using DebugTools;
using Enums;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace Detection
{
    public class ArUcoDetector : MonoBehaviour
    {
        // Settings
        [Header("Settings")]
        [SerializeField] private ArUcoDictionaries dictionaryId = ArUcoDictionaries.ArUco6X6N50;
    
        [Header("Dependencies")]
        [SerializeField] private CameraController cameraController;
    
        public event Action<List<MarkerDetectionResult>> OnMarkersDetected;
    
        private List<MarkerDetectionResult> detectedMarkers = new();
        private int cameraWidth, cameraHeight;
    
        // DLL imports
        [DllImport("aruco")] private static extern void ArucoInit(int dictionaryId);
        [DllImport("aruco")] private static extern void ArucoSetWhitelist(int[] markerIds, int count);
        [DllImport("aruco")] private static extern void ArucoSetCameraIntrinsics(
            float fx, float fy, float cx, float cy);
        [DllImport("aruco")] private static extern void ArucoProcess(IntPtr rgba, int width, int height);
        [DllImport("aruco")] private static extern int ArucoGetCount();
        [DllImport("aruco")] private static extern int ArucoGetId(int index);
        [DllImport("aruco")] private static extern int ArucoGetCornersByID(int arucoId, float[] cornerX, float[] cornerY);
        [DllImport("aruco")] private static extern bool ArucoGetCenterByID(int arucoId, out float centerX, out float centerY);
        [DllImport("aruco")] private static extern bool ArucoEstimatePose(
            int arucoId, float markerSize, float[] rvec, float[] tvec);

        private void Start()
        {
            ArucoInit((int)dictionaryId);
            DebugController.ConsoleLog(this, "<ArUcoDetector>: Initialized");
        }

        private void OnEnable()
        {
            if (cameraController == null)
                return;
        
            cameraController.OnFrame += OnFrame;
            cameraController.OnIntrinsicsUpdated += OnIntrinsicsUpdated;

            if (cameraController.hasIntrinsics)
            {
                OnIntrinsicsUpdated(cameraController.cameraIntrinsics);
            }
        }

        private void OnDisable()
        {
            if (cameraController == null)
                return;
        
            cameraController.OnFrame -= OnFrame;
            cameraController.OnIntrinsicsUpdated -= OnIntrinsicsUpdated;
        }

        private void OnIntrinsicsUpdated(XRCameraIntrinsics? intrinsics)
        {
            if (!intrinsics.HasValue)
                return;
        
            DebugController.ConsoleLog(this, $"Setting intrinsics: fx={intrinsics.Value.focalLength.x}, " +
                                             $"fy={intrinsics.Value.focalLength.y}, " +
                                             $"cx={intrinsics.Value.principalPoint.x}, " +
                                             $"cy={intrinsics.Value.principalPoint.y}");
            ArucoSetCameraIntrinsics(
                intrinsics.Value.focalLength.x, intrinsics.Value.focalLength.y,
                intrinsics.Value.principalPoint.x, intrinsics.Value.principalPoint.y);
        }
    
        private void OnFrame(byte[] frame, int w, int h)
        {
            // after rotation
            cameraWidth = w;
            cameraHeight = h;

            detectedMarkers.Clear();

            GCHandle hdl = GCHandle.Alloc(frame, GCHandleType.Pinned);
            try
            {
                ArucoProcess(hdl.AddrOfPinnedObject(), w, h);
            }
            finally
            {
                hdl.Free();
            }

            int count = ArucoGetCount();

            for (int i = 0; i < count; i++)
            {
                int arucoId = ArucoGetId(i);
            
                var result = new MarkerDetectionResult
                {
                    ID = arucoId,
                    Corners = new Vector2[4],
                    Timestamp = Time.time
                };

                float[] cornerX = new float[4];
                float[] cornerY = new float[4];

                if (ArucoGetCornersByID(arucoId, cornerX, cornerY) == 4)
                {
                    for (int c = 0; c < 4; c++)
                    {
                        result.Corners[c] = new Vector2(cornerX[c], cornerY[c]);
                    }
                }

                detectedMarkers.Add(result);
            }

            if (detectedMarkers.Count > 0)
            {
                OnMarkersDetected?.Invoke(detectedMarkers);
            }
        }

        public void SetWhitelist(List<int> arucoIds)
        {
            if (arucoIds == null || arucoIds.Count == 0)
            {
                ArucoSetWhitelist(null, 0);
                DebugController.ConsoleLog(this, "<ArUcoDetector>: WhiteList disabled");
            }
            else
            {
                ArucoSetWhitelist(arucoIds.ToArray(), arucoIds.Count);
                DebugController.ConsoleLog(this, $"<ArUcoDetector>: WhiteList enabled({string.Join(", ", arucoIds)})");
            }
        }
    
        public bool EstimatePose(int arucoId, float markerSize, out Vector3 rotation, out Vector3 translation)
        {
            rotation = Vector3.zero;
            translation = Vector3.zero;
        
            float[] rvec = new float[3];
            float[] tvec = new float[3];
        
            bool success = ArucoEstimatePose(arucoId, markerSize, rvec, tvec);
            if (success)
            {
                rotation = new Vector3(rvec[0], rvec[1], rvec[2]);
                translation = new Vector3(tvec[0], tvec[1], tvec[2]);
            }
        
            return success;
        }
    
        public bool GetMarkerCenter(int arucoId, out Vector2 center)
        {
            center = Vector2.zero;
            return ArucoGetCenterByID(arucoId, out center.x, out center.y);
        }
    
        public List<MarkerDetectionResult> GetDetectedMarkers()
        {
            return new List<MarkerDetectionResult>(detectedMarkers);
        }
    
        public bool IsMarkerDetected(int arucoId)
        {
            return detectedMarkers.Exists(m => m.ID == arucoId);
        }

        // Helpers
        public Vector2Int GetCameraDimensions()
        {
            return new Vector2Int(cameraWidth, cameraHeight);
        }

        public Vector2 PixelToNormalized(Vector2 pixelCoords)
        {
            // return pixelCoords;
        
            if (cameraWidth == 0 || cameraHeight == 0)
                return Vector2.zero;
        
            float cameraAspect = (float)cameraWidth / cameraHeight;  // 1.33
            float screenAspect = (float)Screen.width / Screen.height; // 1.6

            // 1. Normalize camera pixels to [0, 1]
            float normX = pixelCoords.x / cameraWidth;
            float normY = pixelCoords.y / cameraHeight;

            // crop by height
            float visibleHeight = cameraWidth / screenAspect;
            float cropTop = (cameraHeight - visibleHeight) * 0.5f;
        
            normY = (pixelCoords.y - cropTop) / visibleHeight;

            normY = 1f - normY;

            return new Vector2(normX, normY);
        }

        public Vector2 GetMarkerCenter(MarkerDetectionResult marker)
        {
            Vector2 center = Vector2.zero;
            foreach (var corner in marker.Corners)
            {
                center += corner;
            }

            return center / 4f;
        }
    }
}