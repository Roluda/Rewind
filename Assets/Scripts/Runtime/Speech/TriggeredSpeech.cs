using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rewind
{
    public class TriggeredSpeech : ReverseBehaviour
    {
        [SerializeField]
        string playerTag = "Player";
        [SerializeField]
        SpeechBubble speechBubble;
        [SerializeField]
        LayerMask playerLayer;
        [Header("Pretime Values")]
        [SerializeField]
        float triggerTime;
        [SerializeField]
        bool triggered;

        public override void ForwardUpdate(TimeData timeData)
        {
        }

        public override void ReverseUpdate(TimeData timeData)
        {
            if(timeData.StreamTime <= triggerTime && triggered)
            {
                triggered = false;
                GodSpeechProvider.Instance.Untrigger();
            }
        }

        private void OnDestroy()
        {
            if (TimeStream.Instance)
            {
                TimeStream.Instance.Unregister(this);
            }
        }

        private void Awake()
        {
            TimeStream.Instance.Register(this);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(playerTag) && TimeStream.Forward && !triggered)
            {
                var playerDirection = other.bounds.center - transform.position;
                Physics.Raycast(transform.position, playerDirection, out var hit, Mathf.Infinity, playerLayer, QueryTriggerInteraction.Ignore);
                Debug.DrawRay(transform.position, playerDirection * 3);
                if (hit.collider.transform.root != other.transform.root)
                {
                    Debug.Log("PlayerOccluded");
                    return;
                }

                triggered = true;
                triggerTime = TimeStream.StreamTime;

                var speech = GodSpeechProvider.Instance.TriggerNext();
                if (speech.audio)
                {
                    AudioSource.PlayClipAtPoint(speech.audio, transform.position, speech.volume);
                }
                speechBubble.Show(speech.text);
            }
        }
    }
}
