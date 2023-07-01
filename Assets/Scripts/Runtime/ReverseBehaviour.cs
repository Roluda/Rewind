using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public abstract class ReverseBehaviour : MonoBehaviour
    {
        public abstract void ForwardUpdate(TimeData timeData);
        public abstract void ReverseUpdate(TimeData timeData);
    }
}
