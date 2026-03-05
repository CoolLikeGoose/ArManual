using DebugTools;
using UI;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Tracking.InteractionPoints
{
    public class InteractionManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float maxRaycastDistance = 3f;

        [Header("Dependencies")]
        [SerializeField] private DetailedViewController detailedViewController;

        private Camera cam;
        private bool raycastEnabled = true;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (!raycastEnabled) return;
        
            if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
            {
                var touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];

                if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    RayCastScene(touch.screenPosition);
                }
            }
        }

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            detailedViewController.OnClose += EnableRaycast;
        }

        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
            detailedViewController.OnClose -= EnableRaycast;
        }

        private void RayCastScene(Vector2 touchPosition)
        {
            Ray ray = cam.ScreenPointToRay(touchPosition);
        
            if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance))
            {
                InteractionPointController point =
                    hit.collider.gameObject.GetComponentInParent<InteractionPointController>();

                if (point != null)
                {
                    DebugController.Log(this, "Clicked " + point.iPointId);
                    detailedViewController.OpenTextPanel(point);
                    raycastEnabled = false;
                }
            }
        }

        private void EnableRaycast()
        {
            raycastEnabled = true;
        }
    }
}
