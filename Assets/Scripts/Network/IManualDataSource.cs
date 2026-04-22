using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Network
{
    public interface IManualDataSource
    {
        Task<ManualModel> LoadManual(int manualID);
        Task<List<ScenarioInteractionModel>> LoadScenarioInteractions(int scenarioID);
        Task<List<InteractionPointModel>> LoadInteractionPointsBatch(List<int> interactionPointIDs);
        Task<List<TrackPointModel>> LoadTrackPointsBatch(List<int> trackPointIDs);
    }
}