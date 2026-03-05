using System;
using System.Collections.Generic;
using Models;

namespace DOT
{
    [Serializable]
    public class ScenarioGroup
    {
        public string category;
        public List<ScenarioModel> scenarios;
    }
}