using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class ReversibleBGM : ReverseBehaviour
    {
        [SerializeField]
        AudioSource source;

        int currentClip;

        public override void ForwardUpdate(TimeData timeData)
        {
        }

        public override void ReverseUpdate(TimeData timeData)
        {

        }


        private void Update()
        {
            source.pitch = TimeStream.Forward ? 1 : -1;
        }
    }
}
