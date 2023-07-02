using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class TimeStream : MonoBehaviour
    {
        private static TimeStream instance;
        public static TimeStream Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<TimeStream>();
                }
                return instance;
            }
        }
        public static bool Playing { get; private set; }
        public static bool Forward { get; private set; }
        public static float StreamTime { get; private set; }

        public float ForwardScale => forwardTimeScale;
        public float BackwardScale => backwardTimeScale;


        [SerializeField]
        float forwardTimeScale;
        [SerializeField]
        float backwardTimeScale;

        private List<ReverseBehaviour> reverseBehaviours = new List<ReverseBehaviour>();
        private float rewindedTime;
        private float rewindTarget;

        public event Action OnTimeZero;
        public event Action OnRevindDone;

        public void SetTime(float streamTime)
        {
            StreamTime = streamTime;
        }

        public void Pause()
        {
            Playing = false;
        }

        public void Play()
        {
            Playing = true;
            Forward = true;
        }

        public void Rewind(float time)
        {
            Playing = true;
            rewindedTime = 0;
            rewindTarget = time;
            Forward = false;
        }

        public void Register(ReverseBehaviour reverseBehaviour)
        {
            reverseBehaviours.Add(reverseBehaviour);
        }

        public void Unregister(ReverseBehaviour reverseBehaviour)
        {
            reverseBehaviours.Remove(reverseBehaviour);
        }

        private void FixedUpdate()
        {
            if (!Playing)
                return;

            if (Forward)
            {
                float fixedForwardDeltaTime = Time.fixedDeltaTime * forwardTimeScale;
                StreamTime += fixedForwardDeltaTime;
                var timeData = new TimeData(StreamTime, fixedForwardDeltaTime);
                foreach (var reverseBehaviour in reverseBehaviours)
                {
                    reverseBehaviour.ForwardUpdate(timeData);
                }
            }

            if (!Forward)
            {
                float fixedBackwardDeltaTime = Time.fixedDeltaTime * backwardTimeScale;
                rewindedTime += fixedBackwardDeltaTime;
                if(rewindedTime >= rewindTarget)
                {
                    Forward = true;
                    OnRevindDone?.Invoke();
                }
                if(fixedBackwardDeltaTime >= StreamTime)
                {
                    Forward = true;
                    StreamTime = 0;
                    OnRevindDone?.Invoke();
                    OnTimeZero?.Invoke();
                }
                else
                {
                    StreamTime -= fixedBackwardDeltaTime;
                }

                var timeData = new TimeData(StreamTime, fixedBackwardDeltaTime);
                foreach (var reverseBehaviour in reverseBehaviours)
                {
                    reverseBehaviour.ReverseUpdate(timeData);
                }
            }
        }
    }
}
