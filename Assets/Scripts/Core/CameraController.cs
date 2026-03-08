using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Core
{
    public class CameraController : MonoBehaviour
    {
        [Header("Settings")]
        [Header("Intrinsics")]
        [SerializeField] private bool continuousIntrinsics = false;
        private XRCameraIntrinsics? intrinsics;
        private bool intrinsicsInitialized = false;
    
        [Header("Image capture")]
        [SerializeField] private float frameProcessInterval = 0.3f;
        private float lastFrame;
    
        [Header("Dependencies")]
        [SerializeField] private ARCameraManager cameraManager;
        [SerializeField] private FreezeManager freezeManager;
    
        public event Action<byte[], int, int> OnFrame;
        public event Action<XRCameraIntrinsics?> OnIntrinsicsUpdated;
    
        public XRCameraIntrinsics? cameraIntrinsics => intrinsics;
        public bool hasIntrinsics => intrinsics.HasValue;
        
        private bool isPaused = false;

        private void OnEnable()
        {
            cameraManager.frameReceived += OnCameraFrame;
            freezeManager.OnFreeze += OnPause;  
        }

        private void OnDisable()
        {
            cameraManager.frameReceived -= OnCameraFrame;
            freezeManager.OnFreeze -= OnPause; 
        }

        private void ProcessIntrinsics()
        {
            if (!intrinsicsInitialized || continuousIntrinsics)
            {
                if (cameraManager.TryGetIntrinsics(out XRCameraIntrinsics intrinsicsNew))
                {
                    bool shouldUpdate = !intrinsics.HasValue ||
                                        (continuousIntrinsics && !IntrinsicsEqual(intrinsics.Value, intrinsicsNew));
                    if (shouldUpdate)
                    {
                        intrinsics = intrinsicsNew;
                        intrinsicsInitialized = true;
                
                        OnIntrinsicsUpdated?.Invoke(intrinsics.Value);
                    }
                }
            }
        }

        private void ProcessFrame(ARCameraFrameEventArgs eventArgs)
        {
            if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                return;
            }
        
            var textures = eventArgs.textures;
            if (textures.Count == 0)
            {
                image.Dispose();
                return;
            }
        
            var conversionParams = new XRCpuImage.ConversionParams()
            {
                //Entire Image
                inputRect = new RectInt(0, 0, image.width, image.height),
            
                //downsample
                outputDimensions = new Vector2Int(image.width, image.height),
            
                //Select RGBA
                //TODO:Check with R8
                outputFormat = TextureFormat.RGBA32,
            
                //Mirror image
                // transformation = XRCpuImage.Transformation.MirrorX
            };
        
            // define final size of image in bytes
            int size = image.GetConvertedDataSize(conversionParams);
            var buffer = new NativeArray<byte>(size, Allocator.Temp);

            try
            {
                image.Convert(conversionParams, buffer);
            
                //TODO: making a copy is not good for performance
                byte[] frame = buffer.ToArray();
            
                OnFrame?.Invoke(frame, conversionParams.outputDimensions.x, conversionParams.outputDimensions.y);
            }
            finally
            {
                buffer.Dispose();
                image.Dispose();
            }
        }

        private void OnCameraFrame(ARCameraFrameEventArgs eventArgs)
        {
            ProcessIntrinsics();
        
            if (Time.time - lastFrame < frameProcessInterval) return;
            lastFrame = Time.time;
            
            if (isPaused) return;
            
            ProcessFrame(eventArgs);
        }

        private void OnPause(bool pauseStatus)
        {
            isPaused = pauseStatus;
        }
    
        private bool IntrinsicsEqual(XRCameraIntrinsics a, XRCameraIntrinsics b)
        {
            return Vector2.Distance(a.focalLength, b.focalLength) < 0.01f &&
                   Vector2.Distance(a.principalPoint, b.principalPoint) < 0.01f &&
                   a.resolution == b.resolution;
        }
    }
}
