using System;
using System.Collections.Generic;
using System.Linq;
using DOT;
using Models;
using UI;
using UnityEngine;

namespace Core
{
    public class ScenarioManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ScenarioList scenarioList;

        public ScenarioModel currentScenario {get; private set;}
        public ManualModel currentManual {get; private set;}
        
        private readonly List<ScenarioGroup> groups = new();
        
        public event Action<ScenarioModel> OnScenarioChanged; 

        private void OnEnable()
        {
            scenarioList.OnScenarioChanged += ChangeScenario;
        }

        private void OnDisable()
        {
            scenarioList.OnScenarioChanged -= ChangeScenario;
        }

        public void SetManual(ManualModel manual)
        {
            currentManual = manual;
            BuildGroups(manual);
        }

        private void BuildGroups(ManualModel manualModel)
        {
            groups.Clear();
            
            if (manualModel?.scenarios == null) 
                return;
            
            //TODO: cover sort with tests
            var orderedGroups = manualModel.scenarios
                .GroupBy(s => s.category)
                .OrderBy(g => g.Key);

            foreach (var group in orderedGroups)
            {
                var items = group
                    .OrderBy(s => s.order)
                    .ToList();
                
                groups.Add(new ScenarioGroup
                {
                    category = group.Key,
                    scenarios = items
                });
            }
            
            PopulateScenarioList();
        }

        public ScenarioModel SelectFirstScenario()
        {
            currentScenario = groups[0].scenarios[0];
            return currentScenario;
        }

        private void PopulateScenarioList()
        {
            scenarioList.PopulateList(groups);
        }

        private void ChangeScenario(ScenarioModel scenario)
        {
            if (currentScenario == scenario) 
                return;
            
            currentScenario = scenario;
            OnScenarioChanged?.Invoke(scenario);
        }

        public void ClearContent()
        {
            currentManual = null;
            currentScenario = null;
            
            groups.Clear();
            scenarioList.ClearContent();
        } 
    }
}
