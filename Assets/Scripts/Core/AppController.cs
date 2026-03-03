using Detection;
using Enums;
using Models;
using Network;
using Tracking.InteractionPoints;
using Tracking.Markers;
using UI;
using UnityEngine;

namespace Core
{
    public class AppController : MonoBehaviour
    {
        public static AppController Instance { get; private set; }
    
        [Header("Settings")]
        [SerializeField] public bool enableDebugLog = true;
        [SerializeField] public bool enableDebugOverlay = true;
    
        [Header("Dependencies")]
        [SerializeField] private QrScanner qrController;
        [SerializeField] private MarkerManager markerManager;
        [SerializeField] private ArUcoDetector markerDetector;
        [SerializeField] private InteractionManager interactionManager;
    
        [Header(">>>UI")]
        [SerializeField] private GameObject qrScannerPanel;
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject arPanel;
    
        private AppState _state;
        private ManualModel _currentManual;

        private void Awake()
        {
            if (Instance != null && Instance != this) 
                Destroy(gameObject);
        
            Instance = this;
            SetState(AppState.Startup);
        }

        private void Start()
        {
            // Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
            OnQRScanning();
        }
    
        //=======State control=======
        public void SetState(AppState newState)
        {
            _state = newState;
            UpdateUI();
        }

        private void UpdateUI()
        {
            qrScannerPanel.SetActive(_state == AppState.QrScan);
            loadingPanel.SetActive(_state == AppState.Loading);
            arPanel.SetActive(_state == AppState.Display);
        }

        private void OnQRScanning()
        {
            qrController.enabled = true;
            SetState(AppState.QrScan);
        }

        public void OnQrScanned(string qrContent)
        {
            qrController.enabled = false;
            SetState(AppState.Loading);
            APILoader.Instance.LoadManual(qrContent);
        }

        public void OnInstructionLoaded(ManualModel data)
        {
            SetState(AppState.Display);
        
            markerDetector.enabled = true;
            markerManager.enabled = true;
            interactionManager.enabled = true;
        
            ScenarioList.Instance.Populate(data);
        
            //TODO: proper scenario manager
            markerManager.LoadScenario(data.scenarios[0]);
            // markerManager.LoadManual(data, APILoader.Instance.LoadTrackPoints(data.trackPoints));
        }

        public void OnScenarioLoaded(ScenarioModel data)
        {
            markerManager.LoadScenario(data);
        }

        public void OnExitBtn()
        {
            markerManager.Clear();
            markerDetector.enabled = false;
            markerManager.enabled = false;
            interactionManager.enabled = false;
        
            OnQRScanning();
        }
        //=======end of State control=======
    }
}