using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        [Header("Debug")]
        [SerializeField]
        float forceDebugPauseTime = 0;

        [Header("Time Settings")]
        [SerializeField]
        float maximumTime;
        [SerializeField]
        float defaultDrainRate;
        [SerializeField]
        float timeGained;
        [SerializeField]
        float rewindedTime;
        [SerializeField]
        float startingPointInTime;
        [SerializeField]
        float initialTimeGain;

        [Header("UI")]
        [SerializeField]
        Hourglass hourglass;
        [SerializeField]
        GameObject choicePanel;
        [SerializeField]
        Button yesButton;
        [SerializeField]
        Button runButton;


        float currentTime;

        private void Awake()
        {
            yesButton.onClick.AddListener(Sacrifice);
            runButton.onClick.AddListener(Run);
        }

        private void Start()
        {
            GainTime(initialTimeGain);
            TimeStream.Instance.OnRevindDone += Forward;
            TimeStream.Instance.OnTimeZero += EnableChoice;
            if (startingPointInTime <= 0)
            {
                EnableChoice();
            }
        }

        private void EnableChoice()
        {
            TimeStream.Instance.Pause();
            choicePanel.gameObject.SetActive(true);
        }

        private void Sacrifice()
        {
            Application.Quit();
        }

        private void Run()
        {
            choicePanel.gameObject.SetActive(false);
            TimeStream.Instance.Play();
        }

        private void Update()
        {
            if(TimeStream.Forward && TimeStream.Playing)
            {
                currentTime -= Time.deltaTime * defaultDrainRate;
                currentTime = Mathf.Clamp(currentTime, 0, maximumTime);
                if (currentTime == 0)
                {
                    TimeStream.Instance.Rewind(rewindedTime);
                }


#if UNITY_EDITOR
                if (forceDebugPauseTime > 0 && TimeStream.StreamTime >= forceDebugPauseTime)
                {
                    Debug.Break();
                }
#endif
            }
            hourglass.SetTarget(currentTime / maximumTime);
        }

        void Forward()
        {
            GainTime(timeGained);
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
