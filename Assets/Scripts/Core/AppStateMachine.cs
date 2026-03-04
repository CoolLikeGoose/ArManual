using System;
using Enums;
using UnityEngine;

namespace Core
{
    public class AppStateMachine : MonoBehaviour
    {
        public AppState currentState { get; private set; } = AppState.Startup;
        
        public event Action<AppState> OnStateChanged;

        public void SetState(AppState newState)
        {
            if (currentState == newState) return;
            
            currentState = newState;
            OnStateChanged?.Invoke(currentState);
        }
    }
}
