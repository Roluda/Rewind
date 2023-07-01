using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rewind
{
    public class ReversibleTransform : ReverseBehaviour
    {
        [SerializeField]
        ReverseTransformAsset _startAsset;

        private Stack<TimedTransform> _timeStack = new Stack<TimedTransform>();

        private void Awake()
        {
            TimeStream.Instance.Register(this);
            if (_startAsset)
            {
                _timeStack = new Stack<TimedTransform>(_startAsset.timedTransform.Reverse());
                if(_timeStack.TryPeek(out var peek))
                {
                    transform.localPosition = peek.Position;
                    transform.localRotation = peek.Rotation;
                    transform.localScale = peek.Scale;
                }
            }
        }

        public override void ReverseUpdate(TimeData timeData)
        {
            while(_timeStack.TryPeek(out var timedTransform) && timedTransform.StreamTime >= timeData.StreamTime)
            {
                var pop = _timeStack.Pop();
                transform.localPosition = pop.Position;
                transform.localRotation = pop.Rotation;
                transform.localScale = pop.Scale;
            }
        }

        public override void ForwardUpdate(TimeData timeData)
        {
            var timedTransform = new TimedTransform
            {
                StreamTime = timeData.StreamTime,
                Position = transform.localPosition,
                Rotation = transform.localRotation,
                Scale = transform.localScale
            };
            _timeStack.Push(timedTransform);
        }

        [Button]
        public void StoreToAsset(string name)
        {
            ReverseTransformAsset.StoreToAsset(_timeStack, name);
        }

        private void OnDestroy()
        {
            if (TimeStream.Instance)
            {
                TimeStream.Instance.Unregister(this);
            }
        }
    }
}
