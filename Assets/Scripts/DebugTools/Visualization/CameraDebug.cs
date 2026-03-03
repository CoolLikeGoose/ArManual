using Core;
using UnityEngine;
using UnityEngine.UI;

namespace DebugTools.Visualization
{
    public class CameraDebug : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private RawImage displayImage; // UI RawImage
    
        [Header("Debug Info")]
        [SerializeField] private Text infoText;
    
        private Texture2D texture;
    
        private void OnEnable()
        {
            cameraController.OnFrame += OnFrameReceived;
        }
    
        private void OnDisable()
        {
            cameraController.OnFrame -= OnFrameReceived;
        }
    
        private void OnFrameReceived(byte[] frame, int width, int height)
        {
            if (texture == null || texture.width != width || texture.height != height)
            {
                if (texture != null) Destroy(texture);
                texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            }
        
            texture.LoadRawTextureData(frame);
            texture.Apply();
        
            displayImage.texture = texture;
        
            if (infoText != null)
            {
                infoText.text = $"Image: {width}×{height}\n" +
                                $"Screen: {Screen.width}×{Screen.height}\n" +
                                $"Orientation: {Screen.orientation}";
            }
        
            DebugController.ConsoleLog(this,$"Frame received: {width}×{height} | Screen: {Screen.width}×{Screen.height}");
        }
    }
}
