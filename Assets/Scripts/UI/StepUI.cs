using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StepUI : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private UButtonController nextStepBtn;
        [SerializeField] private UButtonController prevStepBtn;
        [SerializeField] private GameObject stepAssistPanel;
        [SerializeField] private TextMeshProUGUI stepCounterText;
        [SerializeField] private TextMeshProUGUI stepAssistText;

        public event Action OnNextStepClicked;
        public event Action OnPrevStepClicked;
        
        private int currentStep;
        private int totalSteps;
        
        private int currentMarkerId = -1;
        private int previousMarkerId;

        private void OnEnable()
        {
            nextStepBtn.OnButtonClicked += OnNextStep;
            prevStepBtn.OnButtonClicked += OnPrevStep;
        }

        private void OnDisable()
        {
            nextStepBtn.OnButtonClicked -= OnNextStep;
            prevStepBtn.OnButtonClicked -= OnPrevStep;
        }

        public void OnNextStep()
        {
            OnNextStepClicked?.Invoke();
        }
    
        public void OnPrevStep()
        {
            OnPrevStepClicked?.Invoke();
        }

        public void SetTotalSteps(int newTotalSteps)
        {
            totalSteps = newTotalSteps;
            UpdateUIText();
        }
        
        public void SetCurrentStep(int newStep)
        {
            currentStep = newStep;
            UpdateUIText();
            
            nextStepBtn.SetInteractable(currentStep != totalSteps);
            prevStepBtn.SetInteractable(currentStep != 1);
        }

        private void UpdateUIText()
        {
            stepCounterText.text = $"{currentStep}/{totalSteps}";
        }
        
        public void SetStepAssistMarker(int markerId)
        {
            previousMarkerId = currentMarkerId;
            currentMarkerId = markerId;

            stepAssistText.text = currentMarkerId != previousMarkerId ? 
                $"Find marker {currentMarkerId}. And follow the instructions." : "Follow instructions";
        }

        public void ToggleStepUI(bool isVisible)
        {
            //For layout group update
            nextStepBtn.gameObject.SetActive(isVisible);
            prevStepBtn.gameObject.SetActive(isVisible);
            
            nextStepBtn.SetVisibility(isVisible);
            prevStepBtn.SetVisibility(isVisible);
                
            stepAssistPanel.SetActive(isVisible);
        }
     }
}
