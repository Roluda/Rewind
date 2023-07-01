using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class TimeCollectable : MonoBehaviour
    {
        [SerializeField]
        string playerTag;
        [SerializeField]
        ReversibleDespawn despawn;
        [SerializeField]
        float timeGain;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                TimeController.Instance.GainTime(timeGain);
                despawn.Despawn();
            }
        }
    }
}
