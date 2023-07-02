using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    [CreateAssetMenu(fileName = "Speech_New", menuName = "Speech")]
    public class Speech : ScriptableObject
    {
        [TextArea]
        public string text;
        public AudioClip audio;
        public float volume;
    }
}
