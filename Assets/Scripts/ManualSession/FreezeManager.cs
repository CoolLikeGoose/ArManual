using System;
using System.Collections;
using DebugTools;
using Detection;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace ManualSession
{
    public class FreezeManager : MonoBehaviour
    {
        [Header("Dependencies")] 
        [SerializeField] private ARSession arSession;
        [SerializeField] private ARCameraBackground cameraBackground;
        [SerializeField] private ArUcoDetector markerDetector;
        
        [Header("Dependencies-UI")]
        [SerializeField] private RawImage frozenOverlay;
        [SerializeField] private FreezeBtn freezeBtn;
        [SerializeField] private Canvas canvas;
        
        [Header("Dependencies-Holder")]
        [SerializeField] private GameObject iPointsHolder;

        public event Action<bool> OnFreeze;
        
        private Texture2D frozenTexture;
        private bool isFrozen;
        
        private void OnEnable()
        {
            freezeBtn.OnFreezeToggled += ToggleFreeze;
        }
        
        private void OnDisable()
        {
            freezeBtn.OnFreezeToggled -= ToggleFreeze;

            if (frozenTexture != null)
            {
                Destroy(frozenTexture);
                frozenTexture = null;
            }
        }

        private void ToggleFreeze()
        {
            if (isFrozen)
                Unfreeze();
            else
                Freeze();
        }

        private void Freeze()
        {
            StartCoroutine(FreezeCoroutine());
            
            isFrozen = true;
            OnFreeze?.Invoke(isFrozen);
            DebugController.Log(this,"Session is paused");
        }

        private IEnumerator FreezeCoroutine()
        {
            canvas.gameObject.SetActive(false);
            
            yield return new WaitForEndOfFrame();
            
            if (frozenTexture == null ||
                frozenTexture.width != Screen.width ||
                frozenTexture.height != Screen.height)
            {
                if (frozenTexture != null) 
                    Destroy(frozenTexture);
                frozenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            }
            frozenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            frozenTexture.Apply();
            
            frozenOverlay.texture = frozenTexture;
            frozenOverlay.gameObject.SetActive(true);
            
            canvas.gameObject.SetActive(true);
        }

        private void Unfreeze()
        {
            frozenOverlay.gameObject.SetActive(false);
            
            isFrozen = false;
            OnFreeze?.Invoke(isFrozen);
            DebugController.Log(this,"Session is resumed");
        }

        public Transform GetHolderTransform()
        {
            return iPointsHolder.transform;
        }
    }
}
