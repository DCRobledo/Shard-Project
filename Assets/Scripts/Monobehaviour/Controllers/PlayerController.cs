using Shard.Classes.Entities;
using Shard.Classes.Patterns.Command;
using Shard.Monobehaviour.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shard.Monobehaviour.Controllers 
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject player;

        private Command spaceButton;

        private PlayerInputActions playerInputActions;
        private InputAction movement;

        private PlayerMovement playerMovement;


        private void Awake() {
            player = GameObject.Find("player");
            playerMovement = player.GetComponent<PlayerMovement>();

            AwakeInput();
        }

        private void AwakeInput() {
            playerInputActions = new PlayerInputActions();

            spaceButton = new JumpCommand(playerMovement);
        }

        private void OnEnable() {
            EnableInput();
        }

        private void EnableInput() {
            movement = playerInputActions.Player.Movement;
            movement.Enable();

            playerInputActions.Player.Jump.performed += context => ExecuteCommand(spaceButton);
            playerInputActions.Player.Jump.Enable();
        }

        private void OnDisable() {
            DisableInput();
        }

        private void DisableInput() {
            movement.Disable();
            playerInputActions.Player.Jump.Disable();
        }



        private void ExecuteCommand(Command command) {
            command.Execute();
        }
    }
}


