using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class ScenarioGroup
    {
        public string category;
        public List<ScenarioModel> scenarios;
    }
}