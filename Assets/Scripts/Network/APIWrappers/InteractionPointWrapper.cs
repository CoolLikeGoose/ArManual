using System;
using Models;
using Network.APIWrappers.Common;

namespace Network.APIWrappers
{
    [Serializable]
    public class InteractionPointWrapper
    {
        public int interactionPointID;
        public int trackpointID;
        public Vector3Wrapper position;
        public ContentWrapper content;
        
        public InteractionPointModel ToModel()
        {
            return new InteractionPointModel
            {
                interactionPointID = this.interactionPointID,
                trackpointID = this.trackpointID,
                iPointName = this.content.header,
                position = this.position.ToVector3(),
                content = this.content.text
            };
        }
    }
}