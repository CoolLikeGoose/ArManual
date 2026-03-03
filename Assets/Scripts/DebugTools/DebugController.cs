using System.Collections.Generic;
using Core;
using DebugTools.UI;
using UnityEngine;

namespace DebugTools
{
    public class DebugController : MonoBehaviour
    {
        private static DebugController Instance { get; set; }
    
        private List<string> logs = new List<string>();
        public DebugPanelController debugPanel;

        private static bool _enableDebug = true;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _enableDebug = AppController.Instance.enableDebugLog;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void Log(object caller, string log)
        {
            if (!_enableDebug) return;
        
            ConsoleLog(caller, log);
            if (Instance == null)
            {
                return;
            }
        
            Instance.logs.Add(log);
            Instance.debugPanel?.UpdateLog(log);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void ConsoleLog(object caller, string log)
        {
            if (!_enableDebug) return;
        
            UnityEngine.Debug.Log($"_GSDBG_<{caller.GetType().Name}>: {log}");
        }

        public List<string> GetLogs()
        {
            return logs;
        }
    }
}
