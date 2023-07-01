using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class ReversibleTransform : ReverseBehaviour
    {
        private Stack<TimedTransform> _timeStack = new Stack<TimedTransform>();

        private void Awake()
        {
            TimeStream.Instance.Register(this);
        }

        public override void ReverseUpdate(TimeData timeData)
        {
            while(_timeStack.TryPeek(out var timedTransform) && timedTransform.StreamTime >= timeData.StreamTime)
            {
                var pop = _timeStack.Pop();
                transform.position = pop.Position;
                transform.rotation = pop.Rotation;
                transform.localScale = pop.Scale;
            }
        }

        public override void ForwardUpdate(TimeData timeData)
        {
            var timedTransform = new TimedTransform
            {
                StreamTime = timeData.StreamTime,
                Position = transform.position,
                Rotation = transform.rotation,
                Scale = transform.localScale
            };
            _timeStack.Push(timedTransform);
        }

        private void OnDestroy()
        {
            TimeStream.Instance.Unregister(this);
        }
    }
}
