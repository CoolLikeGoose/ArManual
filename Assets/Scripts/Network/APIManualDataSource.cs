using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Network
{
    public class APIManualDataSource : IManualDataSource
    {
        private const string baseUrl = "http://192.168.0.2:8000";
        private const int timeoutSeconds = 10;
        
        public Task<ManualModel> LoadManual(int manualID)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ScenarioInteractionModel>> LoadScenarioInteractions(int scenarioID)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<InteractionPointModel>> LoadInteractionPointsBatch(List<int> interactionPointIDs)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TrackPointModel>> LoadTrackPointsBatch(List<int> trackPointIDs)
        {
            throw new System.NotImplementedException();
        }
    }
}