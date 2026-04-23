using System;
using UnityEngine;

namespace Network.APIWrappers.Common
{
    [Serializable]
    public class Vector3Wrapper
    {
        public float x;
        public float y;
        public float z;

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}