using System;
using Enums;
using UnityEngine;

namespace Core
{
    public class AppUiStateView : MonoBehaviour
    {
        [Header("Dependencies/Scripts")]
        [SerializeField] private AppStateMachine stateMachine;
        
        [Header("Dependencies/Objects")]
        [SerializeField] private GameObject qrScannerPanel;
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject arPanel;

        private void OnEnable()
        {
            stateMachine.OnStateChanged += OnStateChanged;
        }
        
        private void OnDisable()
        {
            stateMachine.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(AppState newState)
        {
            qrScannerPanel.SetActive(newState == AppState.QrScan);
            loadingPanel.SetActive(newState == AppState.Loading);
            arPanel.SetActive(newState == AppState.Display);
        }
    }
}
