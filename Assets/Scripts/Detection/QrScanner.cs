using Core;
using DebugTools;
using UnityEngine;
using ZXing;

namespace Detection
{
    public class QrScanner : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private AppCoordinator appCoordinator;
    
        private Texture2D cameraImageTexture;
    
        private IBarcodeReader barcodeReader = new BarcodeReader()
        {
            AutoRotate = false,
            Options = new ZXing.Common.DecodingOptions()
            {
                TryHarder = false
            }
        };

        private Result result;

        private void OnEnable()
        {
            cameraController.OnFrame += TryDecode;
        }

        private void OnDisable()
        {
            cameraController.OnFrame -= TryDecode;

            if (cameraImageTexture != null)
            {
                Destroy(cameraImageTexture);
                cameraImageTexture = null;
            }
        }
    
        //TODO: Optimize this peace of laggy code(add timeout and texture check)
        private void TryDecode(byte[] frame, int width, int height)
        {
            // Convert Data to texture
            if (cameraImageTexture == null ||
                cameraImageTexture.width != width ||
                cameraImageTexture.height != height)
            {
                if (cameraImageTexture != null) Destroy(cameraImageTexture);
                cameraImageTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            }
        
            cameraImageTexture.LoadRawTextureData(frame);
            cameraImageTexture.Apply();
        
            // Decode barcode
            result = barcodeReader.Decode(
                cameraImageTexture.GetPixels32(),
                cameraImageTexture.width,
                cameraImageTexture.height);

            if (result == null) return;
        
            string resultText = result.Text + " " + result.BarcodeFormat;
            DebugController.Log(this, resultText);
            appCoordinator.OnQrScanned(result.Text);
        }
    }
}
