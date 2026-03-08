using System;
using UnityEngine;

namespace UI
{
    public class FreezeBtn : MonoBehaviour
    {
        public event Action OnFreezeToggled;
        public void ToggleFreeze()
        {
            OnFreezeToggled?.Invoke();
        }
    }
}
