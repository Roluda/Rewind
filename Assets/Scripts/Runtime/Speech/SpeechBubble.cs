using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rewind
{
    public class SpeechBubble : ReverseBehaviour
    {
        [SerializeField]
        TMP_Text text;
        [SerializeField]
        float letterAddInterval;

        [Header("Pretime Values")]
        [SerializeField]
        string targetText = "";
        [SerializeField]
        string currentText = "";
        [SerializeField]
        float timeOfTextEnd;
        [SerializeField]
        float maxDisplayDuration = 3f;

        float timer;
        float idleTimer;

        private void Awake()
        {
            TimeStream.Instance.Register(this);
            text.text = currentText;
        }

        private void OnDestroy()
        {
            if (TimeStream.Instance)
            {
                TimeStream.Instance.Unregister(this);
            }
        }

        public override void ForwardUpdate(TimeData timeData)
        {
            if(targetText.Length > 0)
            {
                timer += timeData.DeltaTime;
                if(timer >= letterAddInterval)
                {
                    idleTimer = 0;
                    timer = 0;
                    currentText += targetText[0];
                    targetText = targetText.Remove(0, 1);
                    text.text = currentText;
                    if(targetText.Length == 0)
                    {
                        timeOfTextEnd = timeData.StreamTime;
                    }
                }
            }
            else
            {
                timer = 0;
            }
        }

        public override void ReverseUpdate(TimeData timeData)
        {
            if(timeData.StreamTime <= timeOfTextEnd && currentText.Length > 0)
            {
                timer += timeData.DeltaTime;
                if(timer >= letterAddInterval)
                {
                    idleTimer = 0;
                    timer = 0;
                    currentText = currentText.Remove(currentText.Length - 1, 1);
                    text.text = currentText;
                }
            }
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
            idleTimer += Time.deltaTime;
            if(idleTimer > maxDisplayDuration)
            {
                currentText = "";
                text.text = "";
            }
        }

        public void Show(string text)
        {
            currentText = "";
            this.text.text = currentText;
            targetText = text;
        }
    }
}
