using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Tests;

namespace Network
{
    public class FakeManualDataSource : IManualDataSource
    {
        private ManualModel currentManual;
        
        public FakeManualDataSource(ManualModel manual)
        {
            currentManual = manual;
        }
        
        public async Task<ManualModel> LoadManual(int manualID)
        {
            await Task.Delay(500);
            // return DummyData.Manuals.Find(m => m.manualID == manualID) ?? DummyData.Manual2;
            return currentManual;
        }

        public async Task<List<ScenarioInteractionModel>> LoadScenarioInteractions(int scenarioID)
        {
            await Task.Delay(6);
            return currentManual.scenarios.Find(s => s.scenarioID == scenarioID).Interactions;
        }

        public async Task<List<InteractionPointModel>> LoadInteractionPointsBatch(List<int> interactionPointIDs)
        {
            await Task.Delay(6);
            return 
                DummyData.InteractionPoints.FindAll(iPoint => interactionPointIDs.Contains(iPoint.interactionPointID));
        }

        public async Task<List<TrackPointModel>> LoadTrackPointsBatch(List<int> trackPointIDs)
        {
            await Task.Delay(6);
            return DummyData.TrackPoints.FindAll(tPoint => trackPointIDs.Contains(tPoint.trackpointID));
        }
    }
}