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
        [SerializeField]
        float currentStreamTime = 0;

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
        [SerializeField]
        float consecutiveMultiplier = 2;

        [Header("UI")]
        [SerializeField]
        Hourglass hourglass;
        [SerializeField]
        GameObject choicePanel;
        [SerializeField]
        Button yesButton;
        [SerializeField]
        Button runButton;
        [SerializeField]
        GameObject startPanel;
        [SerializeField]
        Button startButton;

        [Header("Feel")]
        [SerializeField]
        AudioClip timeGainAudioClip;
        [SerializeField]
        AudioClip timeEmptyGong;
        [SerializeField]
        float pauseTime = 1;

        float currentTime;
        float pauseTimer;
        bool triggerRewind;

        int maxMulti = 1;
        int currentMulti = 1;

        private void Awake()
        {
            yesButton.onClick.AddListener(Sacrifice);
            runButton.onClick.AddListener(Run);
            startButton.onClick.AddListener(Run);
        }

        private void Start()
        {
            GainTimeFromCollect(initialTimeGain);
            TimeStream.Instance.SetTime(startingPointInTime);
            TimeStream.Instance.OnRevindDone += Forward;
            TimeStream.Instance.OnTimeZero += EnableChoice;
            if (startingPointInTime <= 0)
            {
                EnableChoice();
            }
            else
            {
                EnableStartDialogue();
            }
        }

        private void EnableChoice()
        {
            TimeStream.Instance.Pause();
            choicePanel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        void EnableStartDialogue()
        {
            TimeStream.Instance.Pause();
            startPanel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        private void Sacrifice()
        {
            Application.Quit();
        }

        private void Run()
        {
            choicePanel.gameObject.SetActive(false);
            startPanel.gameObject.SetActive(false);
            TimeStream.Instance.Play();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if(TimeStream.Forward && TimeStream.Playing)
            {
                currentTime -= Time.deltaTime * defaultDrainRate;
                currentTime = Mathf.Clamp(currentTime, 0, maximumTime);
                if (currentTime == 0)
                {
                    TimeStream.Instance.Rewind(rewindedTime * currentMulti);
                    AudioSource.PlayClipAtPoint(timeEmptyGong, Camera.main.transform.position, 2);
                }


#if UNITY_EDITOR
                if (forceDebugPauseTime > 0 && TimeStream.StreamTime >= forceDebugPauseTime)
                {
                    Debug.Break();
                }
#endif
            }

            currentStreamTime = TimeStream.StreamTime;
            hourglass.SetTarget(currentTime / maximumTime);
        }

        void Forward()
        {
            currentMulti++;
            if (currentMulti > maxMulti)
            {
                currentMulti = maxMulti;
            }
            GainTime(timeGained);
        }

        public void GainTime(float seconds)
        {
            currentTime += seconds;
            AudioSource.PlayClipAtPoint(timeGainAudioClip, Camera.main.transform.position, 2);
        }

        public void GainTimeFromCollect(float seconds)
        {
            currentMulti = 1;
            maxMulti++;
            currentTime += seconds;
            AudioSource.PlayClipAtPoint(timeGainAudioClip, Camera.main.transform.position, 2);
        }

        public void LoseTime(float seconds)
        {
            currentTime -= seconds;
        }
    }
}
