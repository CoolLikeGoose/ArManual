using UnityEngine;

namespace Detection
{
    public class MarkerDetectionResult
    {
        //ArUco ID
        public int ID;
        public Vector2[] Corners;
        public float Timestamp;

        public float sizeInPixels
        {
            get
            {
                if (Corners == null || Corners.Length != 4)
                    return 0f;
            
                float width = Vector2.Distance(Corners[0], Corners[1]);
                float height = Vector2.Distance(Corners[1],  Corners[2]);
                return (width + height) / 2f;
            }
        }
    
        public bool isValid => Corners != null && Corners.Length == 4 && ID >= 0;
    }
}
