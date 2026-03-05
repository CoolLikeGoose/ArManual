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
    public class AppCoordinator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private AppStateMachine stateMachine;
        [SerializeField] private APILoader apiLoader;
        [SerializeField] private ScenarioManager scenarioManager;
        
        [SerializeField] private QrScanner qrController;
        [SerializeField] private MarkerManager markerManager;
        [SerializeField] private ArUcoDetector markerDetector;
        [SerializeField] private InteractionManager interactionManager;

        private void OnEnable()
        {
            apiLoader.OnManualLoaded += OnInstructionLoaded;
            scenarioManager.OnScenarioChanged += OnScenarioLoaded;
        }

        private void OnDisable()
        {
            apiLoader.OnManualLoaded -= OnInstructionLoaded;
            scenarioManager.OnScenarioChanged -= OnScenarioLoaded;
        }

        private void Start()
        {
            OnQRScanning();
        }

        private void OnQRScanning()
        {
            qrController.enabled = true;
            stateMachine.SetState(AppState.QrScan);
        }

        public void OnQrScanned(string qrContent)
        {
            qrController.enabled = false;
            stateMachine.SetState(AppState.Loading);
            apiLoader.LoadManual(qrContent);
        }

        private void OnInstructionLoaded(ManualModel data)
        {
            stateMachine.SetState(AppState.Display);
        
            markerDetector.enabled = true;
            markerManager.enabled = true;
            interactionManager.enabled = true;
        
            scenarioManager.SetManual(data);
        
            //TODO: proper scenario manager
            markerManager.LoadScenario(data.scenarios[0]);
        }

        public void OnScenarioLoaded(ScenarioModel data)
        {
            markerManager.LoadScenario(data);
        }

        public void OnExitBtn()
        {
            markerManager.Clear();
            scenarioManager.ClearContent();
            
            markerDetector.enabled = false;
            markerManager.enabled = false;
            interactionManager.enabled = false;
        
            OnQRScanning();
        }
    }
}
