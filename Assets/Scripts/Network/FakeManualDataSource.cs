using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Tests;

namespace Network
{
    public class FakeManualDataSource : IManualDataSource
    {
        public async Task<ManualModel> LoadManual(int manualID)
        {
            await Task.Delay(500);
            // return DummyData.Manuals.Find(m => m.manualID == manualID) ?? DummyData.Manual2;
            return DummyData.Manual2;
        }

        public async Task<List<ScenarioInteractionModel>> LoadScenarioInteractions(int scenarioID)
        {
            await Task.Delay(500);
            return DummyData.Manual2.scenarios.Find(s => s.scenarioID == scenarioID).Interactions;
        }

        public async Task<List<InteractionPointModel>> LoadInteractionPointsBatch(List<int> interactionPointIDs)
        {
            await Task.Delay(500);
            return 
                DummyData.InteractionPoints.FindAll(iPoint => interactionPointIDs.Contains(iPoint.interactionPointID));
        }

        public async Task<List<TrackPointModel>> LoadTrackPointsBatch(List<int> trackPointIDs)
        {
            await Task.Delay(500);
            return DummyData.TrackPoints.FindAll(tPoint => trackPointIDs.Contains(tPoint.trackpointID));
        }
    }
}