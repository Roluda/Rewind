using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class GameManager : MonoBehaviour
    {


        // Start is called before the first frame update
        void Start()
        {
            //TimeStream.Instance.OnTimeZero += TimeStream.Instance.Pause;
            TimeStream.Instance.Play();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                TimeStream.Instance.Rewind();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                TimeStream.Instance.Pause();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TimeStream.Instance.Play();
            }
        
        }
    }
}
