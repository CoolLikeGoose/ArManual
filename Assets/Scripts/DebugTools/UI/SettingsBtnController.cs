using UnityEngine;

namespace DebugTools.UI
{
    public class SettingsBtnController : MonoBehaviour
    {
        public DebugPanelController debugPanel;

        public void ToggleDebugPanel()
        {
            debugPanel.Toggle();
        }
    }
}
