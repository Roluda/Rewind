using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class GodSpeechProvider : MonoBehaviour
    {
        private static GodSpeechProvider instance;
        public static GodSpeechProvider Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<GodSpeechProvider>();
                }
                return instance;
            }
        }

        [SerializeField]
        int firstSpeech = 0;
        [SerializeField]
        List<Speech> speeches = new List<Speech>();

        private void Start()
        {
            currentSpeech = firstSpeech;
        }

        int currentSpeech;

        public Speech TriggerNext()
        {
            int clampedSpeechIndex = Mathf.Clamp(currentSpeech, 0, speeches.Count - 1);
            currentSpeech++;
            return speeches[clampedSpeechIndex];
        }

        public void Untrigger()
        {
            currentSpeech--;
        }
    }
}
