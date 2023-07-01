using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class RandomWalk : MonoBehaviour
    {
        [SerializeField]
        float moveSpeed = 1f;
        [SerializeField]
        float switchInterval = 1.5f;
        [SerializeField]
        float lerpSpeed = 3f;
        [SerializeField]
        float smoothDamp = 0.1f;


        Vector3 direction = Vector3.zero;
        Quaternion targetRotation = Quaternion.identity;
        Vector3 targetScale = Vector3.one;
        Vector3 scaleRef;
        float timer;

        // Update is called once per frame
        void Update()
        {
            if (!TimeStream.Forward || !TimeStream.Playing)
                return;

            timer += Time.deltaTime;
            if(timer > switchInterval)
            {
                timer = 0;
                direction = Random.onUnitSphere;
                targetScale = Vector3.one * 2 - Random.onUnitSphere;
                targetRotation = Quaternion.LookRotation(Random.onUnitSphere);
            }

            transform.Translate(direction * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
            transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref scaleRef, smoothDamp);        
        }
    }
}
