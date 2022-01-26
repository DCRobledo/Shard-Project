using Shard.Classes.Input;
using Shard.Classes.Patterns.Command;
using Shard.Classes.Patterns.Singleton;
using Shard.Classes.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shard.Classes.Controllers 
{
    public class PlayerController : SingletonUnity
    {
        private static SingletonUnity instance = null;
        public static new PlayerController Instance { get { return (PlayerController) instance; }}

        private GameObject player;

        private Command jumpButton;
        private Command moveButton;
        private Command reCallButton;
        private Command grabButton;

        private InputActions InputActions;
        private InputAction movement;

        private EntityMovement playerMovement;
        private EntityActions playerActions;

        private void Awake() {
            // Init the controller's instance
            instance =  SingletonUnity.Instance;

            // Get the player game object and its components
            player = GameObject.Find("player");

            playerMovement = player.GetComponent<EntityMovement>();
            playerActions = player.GetComponent<EntityActions>();

            // Initialize the input system
            AwakeInput();
        }

        private void AwakeInput() {
            InputActions = new InputActions();

            jumpButton = new JumpCommand(playerMovement);
            moveButton = new MoveCommand(playerMovement);
            reCallButton = new ActionCommand(playerActions);
            grabButton = new ActionCommand(playerActions);
        }

        private void OnEnable() {
            EnableInput();
        }

        private void EnableInput() {
            movement = InputActions.Player.Movement;
            movement.Enable();

            InputActions.Player.Jump.started += context => jumpButton.ExecuteWithParameters(true);
            InputActions.Player.Jump.canceled += context => jumpButton.ExecuteWithParameters(false);
            InputActions.Player.Jump.Enable();

            InputActions.Player.ReCall.performed += context => reCallButton.ExecuteWithParameters(EntityActions.Action.RECALL);
            InputActions.Player.ReCall.Enable();

            InputActions.Player.Grab.performed += context => grabButton.ExecuteWithParameters(EntityActions.Action.GRAB);
            InputActions.Player.Grab.Enable();
        }

        private void OnDisable() {
            DisableInput();
        }

        private void DisableInput() {
            movement.Disable();

            InputActions.Player.Jump.Disable();
            InputActions.Player.ReCall.Disable();
            InputActions.Player.Grab.Disable();
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


