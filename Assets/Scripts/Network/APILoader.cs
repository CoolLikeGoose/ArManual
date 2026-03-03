using System.Collections;
using Core;
using DebugTools;
using Models;
using Tests;
using UnityEngine;

namespace Network
{
    public class APILoader : MonoBehaviour
    {
        public static APILoader Instance {get; private set;}

        private void Awake()
        {
            Instance = this;
        }

        public void LoadManual(string qrCode)
        {
            DebugController.Log(this,"Loading manual: " + qrCode);
            StartCoroutine(FakeLoad(qrCode));
        }

        public InteractionPointModel LoadIPointByID(int id)
        {
            return DummyData.InteractionPoints.Find(iPoint => iPoint.interactionPointID == id);
        }

        public TrackPointModel LoadTrackPointByID(int id)
        {
            return DummyData.TrackPoints.Find(tPoint => tPoint.trackpointID == id);
        }

        private IEnumerator FakeLoad(string qrCode)
        {
            yield return new WaitForSeconds(2f);
        
            int manualId = int.Parse(qrCode.Split('-')[1]);
            ManualModel data = DummyData.Manual2;
            DebugController.Log(this, "Manual loaded - " + data.name);
        
            AppController.Instance.OnInstructionLoaded(data);
        }
    }
}
