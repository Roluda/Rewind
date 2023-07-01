using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Rewind
{
    public class Bro : MonoBehaviour
    {
        [SerializeField]
        NavMeshAgent agent;
        [SerializeField]
        Transform playerTransform;

        [SerializeField]
        float followRadius;
        [SerializeField]
        float idleInterval;

        float timer;

        bool CanUpdate => TimeStream.Forward && TimeStream.Playing;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (CanUpdate)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = idleInterval;
                    var random = Random.insideUnitCircle.normalized;
                    var target = new Vector3(random.x, 0, random.y) * followRadius;
                    agent.SetDestination(playerTransform.position + target);
                }
            }

            agent.enabled = CanUpdate;
        
        }
    }
}
