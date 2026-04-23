using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DebugTools;
using Models;
using Network.APIWrappers;
using Network.APIWrappers.ListWrappers;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class APIManualDataSource : IManualDataSource
    {
        private readonly string baseUrl ;
        
        public APIManualDataSource(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }
        
        public async Task<ManualModel> LoadManual(int manualID)
        {
            string url = $"{baseUrl}/manuals/{manualID}";
            string json = await Get(url);
            return JsonUtility.FromJson<ManualModel>(json);
        }

        public async Task<List<ScenarioInteractionModel>> LoadScenarioInteractions(int scenarioID)
        {
            string url = $"{baseUrl}/scenarios/{scenarioID}/interactions";
            string json = await Get(url);

            var wrapper = JsonUtility.FromJson<ScenarioInteractionListWrapper>(json);
            return wrapper.items;
        }

        public async Task<List<InteractionPointModel>> LoadInteractionPointsBatch(List<int> interactionPointIDs)
        {
            string url = $"{baseUrl}/interactionpoints/batch";

            var body = new IdListWrapper { ids = interactionPointIDs };
            string json = await Post(url, JsonUtility.ToJson(body));

            var wrapper = JsonUtility.FromJson<InteractionPointListWrapper>(json);
            
            var iPoints = new List<InteractionPointModel>();
            foreach (var iPoint in wrapper.items)
            {
                iPoints.Add(iPoint.ToModel());
            }
            return iPoints;
        }

        public async Task<List<TrackPointModel>> LoadTrackPointsBatch(List<int> trackPointIDs)
        {
            string url = $"{baseUrl}/trackpoints/batch";

            var body = new IdListWrapper { ids = trackPointIDs };
            string json = await Post(url, JsonUtility.ToJson(body));

            var wrapper = JsonUtility.FromJson<TrackPointListWrapper>(json);
            return wrapper.items;
        }
        
        // HTTP 
        private async Task<string> Get(string url)
        {
            using UnityWebRequest req = UnityWebRequest.Get(url);
            var op = req.SendWebRequest();
            while (!op.isDone)  
                await Task.Yield();

            if (req.result != UnityWebRequest.Result.Success)
            {
                DebugController.Log(this, $"GET |{url}| request failed: {req.error}");
                throw new Exception($"GET request failed: {req.error}");
            }
                
            return req.downloadHandler.text;
        }
        
        private async Task<string> Post(string url, string body)
        {
            byte[] data = Encoding.UTF8.GetBytes(body);

            UnityWebRequest req = new UnityWebRequest(url, "POST");
            req.uploadHandler = new UploadHandlerRaw(data);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            var op = req.SendWebRequest();
            while (!op.isDone)
                await Task.Yield();

            if (req.result != UnityWebRequest.Result.Success)
            {
                DebugController.Log(this, $"POST |{url}| failed: {req.error}");
                throw new System.Exception($"POST |{url}| failed: {req.error}");
            }
                

            return req.downloadHandler.text;
        } 
    }
}