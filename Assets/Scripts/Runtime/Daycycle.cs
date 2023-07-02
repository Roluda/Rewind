using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class Daycycle : ReverseBehaviour
    {
        [SerializeField]
        Light diagonalLight;
        [SerializeField]
        Material skyboxMaterial;

        [SerializeField]
        float nightTime;
        [SerializeField]
        float minRotation;
        [SerializeField]
        float maxRotation;

        private void Awake()
        {
            TimeStream.Instance.Register(this);
        }

        public override void ForwardUpdate(TimeData timeData)
        {
            UpdateDaylight(timeData.StreamTime);
        }

        public override void ReverseUpdate(TimeData timeData)
        {
            UpdateDaylight(timeData.StreamTime);
        }

        void UpdateDaylight(float streamTime)
        {
            float value = Mathf.Clamp01(streamTime / nightTime);
            float rotation = Mathf.Lerp(minRotation, maxRotation, value);
            diagonalLight.transform.rotation = Quaternion.Euler(diagonalLight.transform.eulerAngles.x, rotation, diagonalLight.transform.eulerAngles.z);

            skyboxMaterial.SetFloat("_Daytime", value);
            skyboxMaterial.SetFloat("_Rotation", value);

        }
    }
}
