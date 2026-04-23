using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DebugTools;
using Models;
using Tests;
using UnityEngine;

namespace Network
{
    public class APILoader : MonoBehaviour
    {
        [SerializeField] private bool useFakeData = true;
        
        private IManualDataSource dataSource;
        // Backend API 
        
        public event Action<ManualModel> OnManualLoaded;
        public event Action<string> OnManualLoadFailed;
        public event Action<InteractionPointModel> OnIPointLoaded;

        private void Awake()
        {
            if (useFakeData)
                dataSource = new FakeManualDataSource();
            else
                dataSource = new APIManualDataSource("http://192.168.0.102:8000");
        }

        public async void LoadManual(int manualId)
        {
            DebugController.Log(this,"Loading manual: " + manualId);
            
            ManualModel data = await dataSource.LoadManual(manualId);
            
            DebugController.Log(this, "Manual loaded - " + data.name);
            
            OnManualLoaded?.Invoke(data);
        }

        public Task<List<ScenarioInteractionModel>> LoadScenarioInteractions(int scenarioId)
        {
            return dataSource.LoadScenarioInteractions(scenarioId);
        }
        
        public Task<List<InteractionPointModel>> LoadIPointsBatch(List<int> ids)
        {
            return dataSource.LoadInteractionPointsBatch(ids);
        }
        
        public Task<List<TrackPointModel>> LoadTrackPointsBatch(List<int> ids)
        {
            return dataSource.LoadTrackPointsBatch(ids);
        }
    }
}
