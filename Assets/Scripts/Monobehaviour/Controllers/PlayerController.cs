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
        private Command wasdButton;

        private PlayerInputActions playerInputActions;
        private InputAction movement;

        private EntityMovement playerMovement;


        private void Awake() {
            player = GameObject.Find("player");
            playerMovement = player.GetComponent<EntityMovement>();

            AwakeInput();
        }

        private void AwakeInput() {
            playerInputActions = new PlayerInputActions();

            spaceButton = new JumpCommand(playerMovement);
            wasdButton = new MoveCommand(playerMovement);
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

        private void FixedUpdate() {
            MovePlayer(movement.ReadValue<Vector2>());
        }


        private void MovePlayer(Vector2 direction) {
            object[] parameters = {direction.x, direction.y};
            
            wasdButton.ExecuteWithParameters(parameters);
        }

        private void ExecuteCommand(Command command) {
            command.Execute();
        }
    }
}


