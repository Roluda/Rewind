using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Rewind
{
    public class VolumeEffect : MonoBehaviour
    {
        ChromaticAberration chromatic;
        FilmGrain filmGrain;
        [SerializeField]
        Volume volume;


        private void Awake()
        {
            volume.profile.TryGet(out chromatic);
            volume.profile.TryGet(out filmGrain);
        }

        // Update is called once per frame
        void Update()
        {
            if (chromatic)
            {
                chromatic.active = TimeStream.Playing && !TimeStream.Forward;
            }
            if (filmGrain)
            {
                filmGrain.active = TimeStream.Playing && !TimeStream.Forward;
            }
        }
    }
}
