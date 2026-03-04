using System;
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
    
        private Texture2D _cameraImageTexture;
    
        private IBarcodeReader _barcodeReader = new BarcodeReader()
        {
            AutoRotate = false,
            Options = new ZXing.Common.DecodingOptions()
            {
                TryHarder = false
            }
        };

        private Result _result;

        private void OnEnable()
        {
            cameraController.OnFrame += TryDecode;
        }

        private void OnDisable()
        {
            cameraController.OnFrame -= TryDecode;
        }
    
        //TODO: Optimize this peace of laggy code(add timeout and texture check)
        private void TryDecode(byte[] frame, int width, int height)
        {
            // Convert Data to texture
            _cameraImageTexture = new Texture2D(
                width,
                height,
                TextureFormat.RGBA32,
                false);
        
            _cameraImageTexture.LoadRawTextureData(frame);
            _cameraImageTexture.Apply();
        
            // Decode barcode
            _result = _barcodeReader.Decode(
                _cameraImageTexture.GetPixels32(),
                _cameraImageTexture.width,
                _cameraImageTexture.height);

            if (_result == null) return;
        
            string result = _result.Text + " " + _result.BarcodeFormat;
            DebugController.Log(this, result);
            appCoordinator.OnQrScanned(_result.Text);
        }
    
        private Color32[] ConvertGrayscaleToColor32(byte[] gray, int width, int height)
        {
            var colors = new Color32[width * height];

            for (int i = 0; i < gray.Length; i++)
            {
                byte v = gray[i];
                colors[i] = new Color32(v, v, v, 255);
            }

            return colors;
        }
    }
}
