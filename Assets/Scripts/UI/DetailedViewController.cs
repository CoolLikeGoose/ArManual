using System;
using TMPro;
using Tracking.InteractionPoints;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DetailedViewController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject textPanel;
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private Button closeButton;

        public event Action OnClose;
    
        private void Start()
        {
            closeButton?.onClick.AddListener(CloseTextPanel);
        }
    
        public void OpenTextPanel(InteractionPointController interactionPoint)
        {
            header.text = interactionPoint.label;
            contentText.text = interactionPoint.contentText;
        
            textPanel.SetActive(true);
        }

        private void CloseTextPanel()
        {
            textPanel.SetActive(false);
            OnClose?.Invoke();
        }
    }
}
