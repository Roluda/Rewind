using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class StaringHead : ReverseBehaviour
    {
        [SerializeField]
        float amplitude;
        [SerializeField]
        float frequency;

        Vector3 startPosition;

        private void Awake()
        {
            TimeStream.Instance.Register(this);
            startPosition = transform.localPosition;
        }

        private void OnDestroy()
        {
            if (TimeStream.Instance)
            {
                TimeStream.Instance.Unregister(this);
            }
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform.position + Vector3.down);
        }


        public override void ForwardUpdate(TimeData timeData)
        {
            WobbleVertical(timeData.StreamTime);
        }

        public override void ReverseUpdate(TimeData timeData)
        {
            WobbleVertical(timeData.StreamTime);
        }

        void WobbleVertical(float time)
        {
            float height = Mathf.Sin(time * frequency) * amplitude;
            transform.localPosition = new Vector3(transform.localPosition.x, startPosition.y + height, transform.localPosition.z);
        }
    }
}
