using System;
using System.Collections.Generic;
using Models;
using Network;
using UnityEngine;

public class TestingThings : MonoBehaviour
{
    [SerializeField] private APILoader apiLoader;

    private void Awake()
    {
        // apiLoader.OnManualLoaded += OnManualLoaded;
    }

    public void OnBtn() 
    {
        Debug.Log("Button pressed");
        DoSmth();
    }

    private async void DoSmth()
    {
        // apiLoader.LoadManual(1);
        
        // var scenarioI = await apiLoader.LoadScenarioInteractions(1);
        // TestScenarioInteractions(scenarioI);
        
        // var trackPoints = await apiLoader.LoadTrackPointsBatch(new List<int> {1, 2, 3});
        // TestTrackPoints(trackPoints);
        
        var ip = await apiLoader.LoadIPointsBatch(new List<int> {100, 101});
        TestInteractionPoints(ip);
    }

    private void OnManualLoaded(ManualModel manual)
    {
        TestManual(manual);
    }

    private void TestManual(ManualModel manual)
    {
        Debug.Log("Manual loaded" +
                  "\n Manual ID: " + manual.manualID +
                  "\n Manual name: " + manual.name +
                  "\n Manual status: " + manual.status +
                  "\n Manual trackPoints: " + manual.trackPoints +
                  "\n Manual scenarios: " + manual.scenarios.Count);

        foreach (var scenario in manual.scenarios)
        {
            Debug.Log("Scenario" +
                      "\n scenarioID: " + scenario.scenarioID +
                      "\n name: " + scenario.name +
                      "\n type: " + scenario.type +
                      "\n category: " + scenario.category +
                      "\n order: " + scenario.order +
                      "\n intcnt: " + scenario.Interactions?.Count);
        }
    }
    
    private void TestTrackPoints(List<TrackPointModel> trackPoints)
    {
        Debug.Log("TrackPoints loaded: " + trackPoints.Count);

        foreach (var tp in trackPoints)
        {
            Debug.Log("TrackPoint" +
                      "\n trackpointID: " + tp.trackpointID +
                      "\n trackpointName: " + tp.trackpointName +
                      "\n arucoID: " + tp.arucoID +
                      "\n sizeCm: " + tp.sizeCm);
        }
    }
    
    private void TestScenarioInteractions(List<ScenarioInteractionModel> interactions)
    {
        Debug.Log("Scenario interactions loaded: " + interactions.Count);

        foreach (var si in interactions)
        {
            Debug.Log("ScenarioInteraction" +
                      "\n interactionID: " + si.interactionID +
                      "\n interactionPointID: " + si.interactionPointID +
                      "\n order: " + si.order +
                      "\n overrideContent: " + si.overrideContent);
        }
    }

    private void TestInteractionPoints(List<InteractionPointModel> points)
    {
        Debug.Log("InteractionPoints loaded: " + points.Count);

        foreach (var ip in points)
        {
            Debug.Log("InteractionPoint" +
                      "\n interactionPointID: " + ip.interactionPointID +
                      "\n trackpointID: " + ip.trackpointID +
                      "\n iPointName: " + ip.iPointName +
                      "\n position: " + ip.position +
                      "\n content text: " + ip.content);
        }
    }

}
