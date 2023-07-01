using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    [Serializable]
    public struct TimedTransform
    {
        public float StreamTime;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    }
}
