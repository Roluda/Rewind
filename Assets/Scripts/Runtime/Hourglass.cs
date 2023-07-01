using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewind
{
    public class Hourglass : MonoBehaviour
    {
        [SerializeField]
        Image current;
        [SerializeField]
        Image gain;
        [SerializeField]
        Image drain;
        [SerializeField]
        float dampSpeed;

        float currentValue;
        float targetValue;
        float drainVelocity;
        float currentVelocity;

        public void SetTarget(float targetValue)
        {
            this.targetValue = targetValue;
        }

        private void Update()
        {
            if(currentValue > targetValue)
            {
                currentValue = targetValue;
                current.fillAmount = currentValue;
                gain.fillAmount = currentValue;
                drain.fillAmount = Mathf.SmoothDamp(drain.fillAmount, currentValue, ref drainVelocity, dampSpeed);
            }

            if(currentValue < targetValue)
            {
                gain.fillAmount = targetValue;
                currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref currentVelocity, dampSpeed);
                current.fillAmount = currentValue;
                drain.fillAmount = currentValue;
            }
        }
    }
}
