using System;
using System.Collections.Generic;
using System.Linq;
using DebugTools;
using Models;
using UI;
using UnityEngine;

namespace ManualSession
{
    public class StepScenarioManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StepUI stepUI;
        
        /// <summary>
        /// Calls when the user changes a step, sends (nextInteractionPoint)
        /// </summary>
        public event Action<int> OnStepChanged;
        
        private ScenarioModel currentScenario;
        
        private List<ScenarioInteractionModel> currentScenarioInteractions = new();
        private int currentInteraction = 1;
        
        private void OnEnable()
        {
            stepUI.OnNextStepClicked += OnNextStep;
            stepUI.OnPrevStepClicked += OnPrevStep;
            
            stepUI.ToggleStepUI(true);
        }

        private void OnDisable()
        {
            stepUI.OnNextStepClicked -= OnNextStep;
            stepUI.OnPrevStepClicked -= OnPrevStep;
            
            stepUI.ToggleStepUI(false);
        }

        public void SetStepManual(ScenarioModel scenario)
        {
            currentScenarioInteractions.Clear();
            
            currentScenario = scenario;
            currentScenarioInteractions = currentScenario.Interactions
                .OrderBy(i => i.order)
                .ToList();
            currentInteraction = 0;
            
            stepUI.SetTotalSteps(currentScenarioInteractions.Count);
            StepChangedUpdate();
        }

        public void UpdateCurrentMarker(int markerId)
        {
            DebugController.Log(this, "Found marker for this step: " + markerId);
            stepUI.SetStepAssistMarker(markerId);
        }
        
        //TODO: change lit of buttons
        private void OnNextStep()
        {
            if (currentInteraction >= currentScenarioInteractions.Count - 1) 
                return;
            
            currentInteraction++;
            StepChangedUpdate();
        }

        private void OnPrevStep()
        {
            if (currentInteraction <= 0) 
                return;
            
            currentInteraction--;
            StepChangedUpdate();
        }

        private void StepChangedUpdate()
        {
            DebugController.Log(this, "Step changed to: " + currentInteraction);
            stepUI.SetCurrentStep(currentInteraction + 1);
            OnStepChanged?.Invoke(currentScenarioInteractions[currentInteraction].interactionPointID);
        } 
    }
}
