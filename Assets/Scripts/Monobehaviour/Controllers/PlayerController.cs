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

        private Command jumpButton;
        private Command moveButton;
        private Command reCallButton;

        private PlayerInputActions playerInputActions;
        private InputAction movement;

        private EntityMovement playerMovement;
        private EntityActions playerActions;


        private void Awake() {
            player = GameObject.Find("player");

            playerMovement = player.GetComponent<EntityMovement>();
            playerActions = player.GetComponent<EntityActions>();

            AwakeInput();
        }

        private void AwakeInput() {
            playerInputActions = new PlayerInputActions();

            jumpButton = new JumpCommand(playerMovement);
            moveButton = new MoveCommand(playerMovement);
            reCallButton = new ReCallCommand(playerActions);
        }

        private void OnEnable() {
            EnableInput();
        }

        private void EnableInput() {
            movement = playerInputActions.Player.Movement;
            movement.Enable();

            playerInputActions.Player.Jump.started += context => jumpButton.ExecuteWithParameters(true);
            playerInputActions.Player.Jump.canceled += context => jumpButton.ExecuteWithParameters(false);
            playerInputActions.Player.Jump.Enable();

            playerInputActions.Player.ReCall.performed += context => reCallButton.Execute();
            playerInputActions.Player.ReCall.Enable();
        }

        private void OnDisable() {
            DisableInput();
        }

        private void DisableInput() {
            movement.Disable();
            playerInputActions.Player.Jump.Disable();
            playerInputActions.Player.ReCall.Disable();
        }

        private void FixedUpdate() {
            MovePlayer(movement.ReadValue<Vector2>());
        }


        private void MovePlayer(Vector2 direction) {
            object[] parameters = {direction.x, direction.y};
            
            moveButton.ExecuteWithParameters(parameters);
        }
    }
}


