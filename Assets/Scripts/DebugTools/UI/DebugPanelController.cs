using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DebugTools.UI
{
    public class DebugPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject debugPanel;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private TextMeshProUGUI debugText;
    

        public void Toggle()
        {
            debugPanel.SetActive(!debugPanel.activeSelf);
            DebugController.Log(this, debugPanel.activeSelf ? "Debug Panel is now active!" : "Debug Panel is now inactive");
        }

        private int counter = 0;
        public void UpdateLog(string log)
        {
            counter++;
            debugText.text += $"[DBG: {counter}]" + log + "\n";
        }
    }
}
