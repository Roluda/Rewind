using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewind
{
    public class WarpFov : MonoBehaviour
    {
        [SerializeField]
        CinemachineVirtualCamera followCamera;
        [SerializeField]
        float warpFov = 23;
        [SerializeField]
        float damp = 0.1f;

        float defaultFOV;
        float fovSpeed;

        private void Awake()
        {
            defaultFOV = followCamera.m_Lens.FieldOfView;
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

            float targetFov = TimeStream.Forward ? defaultFOV : warpFov;
            followCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(followCamera.m_Lens.FieldOfView, targetFov, ref fovSpeed, damp);
        }
    }
}
