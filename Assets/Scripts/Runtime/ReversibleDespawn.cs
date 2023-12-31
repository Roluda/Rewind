using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class ReversibleDespawn : ReverseBehaviour
    {
        [Header("Pretime Values")]
        [SerializeField]
        float despawnTime;
        [SerializeField]
        bool despawned;

        public void Despawn()
        {
            despawnTime = TimeStream.StreamTime;
            despawned = true;
            gameObject.SetActive(false);
        }
        public override void ForwardUpdate(TimeData timeData)
        {
        }

        public override void ReverseUpdate(TimeData timeData)
        {
            if(timeData.StreamTime <= despawnTime && despawned)
            {
                gameObject.SetActive(true);
                despawned = false;
            }
        }

        private void Start()
        {
            TimeStream.Instance.Register(this);
            if (despawned)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (TimeStream.Instance)
            {
                TimeStream.Instance.Unregister(this);
            }
        }
    }
}
