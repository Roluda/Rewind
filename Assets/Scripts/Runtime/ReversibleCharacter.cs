using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rewind
{
    public class ReversibleCharacter : MonoBehaviour
    {
        [SerializeField]
        CharacterController controller;
        [SerializeField]
        FirstPersonController firstPersonController;


        bool CanUpdate => TimeStream.Forward && TimeStream.Playing;

        // Update is called once per frame
        void Update()
        {
            controller.enabled = CanUpdate;
            firstPersonController.enabled = CanUpdate;
        }
    }
}
