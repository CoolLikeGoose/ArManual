using System;
using System.Collections.Generic;
using DebugTools;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScenarioList : MonoBehaviour
    {
        [SerializeField] private GameObject scenarioPrefab;
        [SerializeField] private GameObject headerPrefab;
    
        private Transform content;
        
        public event Action<ScenarioModel> OnScenarioChanged; 

        private void Awake()
        {
            content = GetComponent<Transform>();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void PopulateList(List<ScenarioGroup> scenarioGroups)
        {
            ClearContent();
            
            foreach (var group in scenarioGroups)
            {
                var header = Instantiate(headerPrefab, content);
                var headerText = header.GetComponent<TextMeshProUGUI>();
                if (headerText) headerText.text = group.category;

                foreach (var scenario in group.scenarios)
                {
                    var item = Instantiate(scenarioPrefab, content);
                    item.GetComponentInChildren<TextMeshProUGUI>().text = scenario.name;

                    item.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            DebugController.Log(this, "scenario clicked: " + scenario.name);
                            OnScenarioChanged?.Invoke(scenario);
                        }
                    );
                }
            }
        }

        public void ClearContent()
        {
            for (int i = content.childCount - 1; i >= 0; i--)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }
    }
}
