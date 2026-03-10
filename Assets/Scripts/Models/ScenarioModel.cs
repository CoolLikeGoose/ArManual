using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class ScenarioModel
    {
        public int scenarioID;
        public string name;
        public int type;
        public string category;
        public int order;
        public List<ScenarioInteractionModel> Interactions;
    }
}
