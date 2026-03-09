using UnityEngine;

namespace Core
{
    public class AppController : MonoBehaviour
    {
        public static AppController Instance { get; private set; }
    
        [Header("Settings")]
        [SerializeField] public bool enableDebugLog = true;
        [SerializeField] public bool enableStatusPanel = true;
        [SerializeField] public bool enableDebugOverlay = true;

        private void Awake()
        {
            if (Instance != null && Instance != this) 
                Destroy(gameObject);
        
            Instance = this;
        }

        private void Start()
        {
            // Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}