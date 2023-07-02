using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rewind
{
    public class EnableTouchControlOnTouch : MonoBehaviour
    {
        [SerializeField]
        GameObject mobileTouch;
        [SerializeField]
        InputActionReference touch;

        private void Awake()
        {
            mobileTouch.gameObject.SetActive(Application.isMobilePlatform);
        }

        // Update is called once per frame
        void Update()
        {
            if (touch.action.WasPerformedThisFrame())
            {
                mobileTouch.gameObject.SetActive(true);
            }
        
        }
    }
}
