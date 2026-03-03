using System;

namespace Models
{
    [Serializable]
    public class ScenarioInteractionModel
    {
        public int interactionID;
        public int interactionPointID;
        public int order = 1;
        public string overrideContent;
    }
}
