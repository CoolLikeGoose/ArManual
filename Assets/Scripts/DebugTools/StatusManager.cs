using System.Collections.Generic;
using System.Text;
using Core;
using TMPro;
using UnityEngine;

namespace DebugTools
{
    public class StatusManager : MonoBehaviour
    {
        public static StatusManager Instance { get; private set; }
        
        [Header("Dependencies-UI")]
        [SerializeField] private GameObject statusPanel;
        [SerializeField] private TextMeshProUGUI statusText;
        
        private float updateInterval = 0.1f;

        private class MarkerDebugInfo
        {
            public bool isActive;
            public float screenSize;
            public float zDistance;

            public MarkerDebugInfo()
            {
                isActive = false;
                screenSize = 0;
                zDistance = 0;
            }
        }

        private Dictionary<int, MarkerDebugInfo> markersInfo = new();
        
        private float lastUpdate;
        private StringBuilder sb = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            if (statusPanel != null)
            {
                statusPanel.SetActive(AppController.Instance.enableStatusPanel);       
            }
            
            UpdateDisplay();
        }

        private void Update()
        {
            if (Time.time - lastUpdate > updateInterval)
            {
                UpdateDisplay();
                lastUpdate = Time.time;
            }
        }

        public void UpdateMarker(int markerID, bool isActive, float screenSize, float zDistance)
        {
            if (!markersInfo.ContainsKey(markerID))
            {
                markersInfo.Add(markerID, new MarkerDebugInfo());
            }
            
            markersInfo[markerID].isActive = isActive;
            markersInfo[markerID].screenSize = screenSize;
            markersInfo[markerID].zDistance = zDistance;    
        }

        public void UpdateMarker(int markerID, bool isActive)
        {
            if (!markersInfo.ContainsKey(markerID))
            {
                markersInfo.Add(markerID, new MarkerDebugInfo());
            }
            
            markersInfo[markerID].isActive = isActive;
        }
        
        public void UpdateMarker(int markerID, float screenSize)
        {
            if (!markersInfo.ContainsKey(markerID))
            {
                markersInfo.Add(markerID, new MarkerDebugInfo());
            }
            
            markersInfo[markerID].screenSize = screenSize;
        }
        
        public void UpdateMarker(int markerID, bool isActive, float screenSize)
        {
            if (!markersInfo.ContainsKey(markerID))
            {
                markersInfo.Add(markerID, new MarkerDebugInfo());
            }
            
            markersInfo[markerID].isActive = isActive;
            markersInfo[markerID].screenSize = screenSize;
        }
        
        public void UpdateMarker(int markerID, Vector3 relativePosition)
        {
            if (!markersInfo.ContainsKey(markerID))
            {
                markersInfo.Add(markerID, new MarkerDebugInfo());
            }
            
            markersInfo[markerID].zDistance = relativePosition.z;
        }

        private void UpdateDisplay()
        {
            if (statusText == null) return;

            sb.Clear();
            sb.AppendLine("ID    | Status    | Size   | First Z-Dist");
            sb.AppendLine("------|-----------|--------|-------------");

            foreach (var kvp in markersInfo)
            {
                int id = kvp.Key;
                var info = kvp.Value;
                
                string status = info.isActive ? "Active" : "Inactive";
                
                sb.AppendLine(
                    $"{id,-6}| {status} | " +
                    $"{info.screenSize,6:F1} | " +
                    $"{info.zDistance,6:F2} | "
                );
            }
            
            statusText.text = sb.ToString();
        }
        
        public void Clear()
        {
            markersInfo.Clear();
        }
    }
}
