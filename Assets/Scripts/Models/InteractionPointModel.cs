using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class InteractionPointModel
    {
        public int interactionPointID;
        public int trackpointID;
        public string iPointName;
        public Vector3 position;
        public string content;
    }
}