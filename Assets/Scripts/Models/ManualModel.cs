using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class ManualModel
    {
        public int manualID;
        public string name;
        public string status;
        public int trackPoints;
        public List<ScenarioModel> scenarios;
    }
}
