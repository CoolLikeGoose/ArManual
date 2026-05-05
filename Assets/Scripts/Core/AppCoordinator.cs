using System.Collections.Generic;
using DebugTools;
using Detection;
using Enums;
using ManualSession;
using Models;
using Network;
using Tracking;
using Tracking.Markers;
using UnityEngine;

namespace Core
{
    public class AppCoordinator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private AppStateMachine stateMachine;
        [SerializeField] private APILoader apiLoader;
        [SerializeField] private ScenarioManager scenarioManager;
        [SerializeField] private StepScenarioManager stepScenarioManager;
        
        [SerializeField] private QrScanner qrController;
        [SerializeField] private MarkerManager markerManager;
        [SerializeField] private ArUcoDetector markerDetector;
        [SerializeField] private InteractionManager interactionManager;

        private void OnEnable()
        {
            apiLoader.OnManualLoaded += OnInstructionLoaded;
            scenarioManager.OnScenarioChanged += OnScenarioLoaded;
            stepScenarioManager.OnStepChanged += OnStepChanged;
        }

        private void OnDisable()
        {
            apiLoader.OnManualLoaded -= OnInstructionLoaded;
            scenarioManager.OnScenarioChanged -= OnScenarioLoaded;
            stepScenarioManager.OnStepChanged -= OnStepChanged;
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
            apiLoader.LoadManual(int.Parse(qrContent.Split('-')[1]));
        }

        private async void OnInstructionLoaded(ManualModel data)
        {
            stateMachine.SetState(AppState.Display);
        
            //TODO: implement subsystems controller?
            markerDetector.enabled = true;
            markerManager.enabled = true;
            interactionManager.enabled = true;
        
            scenarioManager.SetManual(data);
            
            ScenarioModel scenario = scenarioManager.SelectFirstScenario();
            var interactions = await markerManager.LoadScenario(scenario);
            OnStepManualLoad(scenario, interactions);
        }

        private async void OnScenarioLoaded(ScenarioModel data)
        {
            var interactions = await markerManager.LoadScenario(data);
            OnStepManualLoad(data, interactions);
        }

        private void OnStepManualLoad(ScenarioModel scenario, List<ScenarioInteractionModel> interactions)
        {
            if (scenario.type != (int)ScenarioType.Step)
            {
                if (stepScenarioManager.enabled) 
                    stepScenarioManager.enabled = false;
                
                return;
            }
            
            DebugController.Log(this, "Step manual is loaded");
            stepScenarioManager.enabled = true;
            stepScenarioManager.SetCurrentInteractions(interactions);
        }

        private void OnStepChanged(int nextInteractionPointId)
        {
            int currentMarker = markerManager.ShowOnly(nextInteractionPointId);
            stepScenarioManager.UpdateCurrentMarker(currentMarker);
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
