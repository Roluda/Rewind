using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public struct TimeData
    {
        public TimeData(float streamTime, float deltaTime)
        {
            StreamTime = streamTime;
            DeltaTime = deltaTime;
        }

        public float StreamTime;
        public float DeltaTime; 
    }
}
