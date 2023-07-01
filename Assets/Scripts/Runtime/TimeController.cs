using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class TimeController : MonoBehaviour
    {
        private static TimeController instance;
        public static TimeController Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<TimeController>();
                }
                return instance;
            }
        }


        [SerializeField]
        Hourglass hourglass;
        [SerializeField]
        float maximumTime;
        [SerializeField]
        float defaultDrainRate;
        [SerializeField]
        float startTime;


        float currentTime;

        private void Start()
        {
            GainTime(startTime);
            TimeStream.Instance.OnRevindDone += Forward;
        }

        private void Update()
        {
            if(TimeStream.Forward && TimeStream.Playing)
            {
                currentTime -= Time.deltaTime * defaultDrainRate;
                currentTime = Mathf.Clamp(currentTime, 0, maximumTime);
                if (currentTime == 0)
                {
                    TimeStream.Instance.Rewind();
                }
            }
            hourglass.SetTarget(currentTime / maximumTime);
        }

        void Forward()
        {
            GainTime(startTime);
        }

        public void GainTime(float seconds)
        {
            currentTime += seconds;
        }

        public void LoseTime(float seconds)
        {
            currentTime -= seconds;
        }



    }
}
