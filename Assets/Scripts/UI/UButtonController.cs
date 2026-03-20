using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UButtonController : MonoBehaviour
    {
        [Header("Startup Settings")]
        
        [Header("Dependencies")]
        [SerializeField] private GameObject content;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Button button;
        
        public event Action OnButtonClicked;

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        public void Show()
        {
            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }
        
        public void SetVisibility(bool visible)
        {
            if (visible) Show();
            else Hide();
        }

        public void SetInteractable(bool interactable)
        {
            button.interactable = interactable;
        }

        private void OnButtonClick()
        {
            OnButtonClicked?.Invoke();
        }
    }
}
