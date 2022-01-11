using Shard.Classes.Entities;
using Shard.Monobehaviour.Entities.Common;
using Shard.Classes.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shard.Monobehaviour.Controllers 
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject player;
        private PlayerMovement playerMovement;

        private Command spaceButton;

        private PlayerInputActions playerInputActions;
        private InputAction movement;


        private void Awake() {
            player = GameObject.Find("player");
            playerMovement = player.GetComponent<PlayerMovement>();
            spaceButton = new JumpCommand(playerMovement);

            playerInputActions = new PlayerInputActions();
        }

        private void OnEnable() {
            movement = playerInputActions.Player.Movement;
            movement.Enable();

            playerInputActions.Player.Jump.performed += context => ExecuteCommand(spaceButton);
            playerInputActions.Player.Jump.Enable();
        }

        private void OnDisable() {
            movement.Disable();
            playerInputActions.Player.Jump.Disable();
        }



        private void ExecuteCommand(Command command) {
            command.Execute();
        }
    }
}


