using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rewind
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float lookSpeedX;

        [SerializeField]
        InputActionReference moveBinding;
        [SerializeField]
        InputActionReference lookBinding;

        bool CanUpdate => TimeStream.Forward && TimeStream.Playing;

        // Update is called once per frame
        void Update()
        {
            var move = moveBinding.action.ReadValue<Vector2>();
            var look = lookBinding.action.ReadValue<Vector2>();
            if (!CanUpdate)
            {
                move = Vector2.zero;
                look = Vector2.zero;
            }

            var rotationX = look.x * Time.deltaTime * lookSpeedX;
            var moveDirection = new Vector3(move.x, 0, move.y);

            transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.Self);
            transform.Rotate(0, rotationX, 0, Space.Self);        
        }
    }
}
