using System;
using System.Collections.Generic;
using Models;

namespace Network.APIWrappers.ListWrappers
{
    [Serializable]
    public class TrackPointListWrapper
    {
        public List<TrackPointModel> items;
    }
}