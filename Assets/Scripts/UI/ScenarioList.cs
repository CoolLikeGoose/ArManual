using System.Linq;
using Core;
using DebugTools;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScenarioList : MonoBehaviour
    {
        //TODO: get rid of singleton
        public static ScenarioList Instance { get; private set; }
        
        [SerializeField] private AppCoordinator appCoordinator;
    
        [SerializeField] private GameObject scenarioPrefab;
        [SerializeField] private GameObject headerPrefab;
    
        private Transform _content;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        
            _content = GetComponent<Transform>();
        }

        public void Populate(ManualModel manualModel)
        {
            ClearContent();
        
            //TODO: Good sort
            var groups = manualModel.scenarios
                .GroupBy(s => s.category)
                .OrderBy(g => g.Key);

            foreach (var group in groups)
            {
                var header = Instantiate(headerPrefab, _content);
                var headerText = header.GetComponent<TextMeshProUGUI>();
                if (headerText) headerText.text = group.Key;

                var items = group.OrderBy(s => s.order);

                foreach (var scenario in items)
                {
                    var item = Instantiate(scenarioPrefab, _content);
                    item.GetComponentInChildren<TextMeshProUGUI>().text = scenario.name;

                    item.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            DebugController.Log(this, "scenario clicked: " + scenario.name);
                            //TODO: create scenario manager
                            appCoordinator.OnScenarioLoaded(scenario);
                        }
                    );
                }
            }
        }

        private void ClearContent()
        {
            for (int i = _content.childCount - 1; i >= 0; i--)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
        }
    }
}
