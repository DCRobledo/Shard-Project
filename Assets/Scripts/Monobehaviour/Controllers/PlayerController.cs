using Shard.Classes.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shard.Monobehaviour.Controllers 
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;
        private InputAction movement;


        private void Awake() {
            playerInputActions = new PlayerInputActions();
        }

        private void OnEnable() {
            movement = playerInputActions.Player.Movement;
            movement.Enable();

            playerInputActions.Player.Jump.performed += Jump;
            playerInputActions.Player.Jump.Enable();
        }

        private void OnDisable() {
            movement.Disable();
            playerInputActions.Player.Jump.Disable();
        }

        private void FixedUpdate() {
            Debug.Log("Movement values: " + movement.ReadValue<Vector2>());
        }


        private void Jump(InputAction.CallbackContext obj) {
            Debug.Log("Jump!");
        }
    }
}


