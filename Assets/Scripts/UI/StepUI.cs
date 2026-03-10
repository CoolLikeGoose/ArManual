using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StepUI : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Button nextStepBtn;
        [SerializeField] private Button prevStepBtn;
        [SerializeField] private GameObject stepAssistPanel;
        [SerializeField] private TextMeshProUGUI stepCounterText;
        [SerializeField] private TextMeshProUGUI stepAssistText;

        public event Action OnNextStepClicked;
        public event Action OnPrevStepClicked;
        
        private int currentStep;
        private int totalSteps;
        
        private int currentMarkerId;
        private int previousMarkerId;
        
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

            nextStepBtn.interactable = currentStep != totalSteps;
            prevStepBtn.interactable = currentStep != 1;
        }

        private void UpdateUIText()
        {
            stepCounterText.text = $"{currentStep}/{totalSteps}";
        }
        
        public void SetStepAssistMarker(int markerId)
        {
            previousMarkerId = currentMarkerId;
            currentMarkerId = markerId;

            if (currentMarkerId != previousMarkerId)
            {
                stepAssistText.text = $"Find marker {currentMarkerId}. And follow the instructions.";       
            }
            stepAssistText.text = "Follow instructions";
        }

        public void ToggleStepUI(bool isVisible)
        {
            nextStepBtn.gameObject.SetActive(isVisible);
            prevStepBtn.gameObject.SetActive(isVisible);
            stepAssistPanel.SetActive(isVisible);
        }
     }
}
