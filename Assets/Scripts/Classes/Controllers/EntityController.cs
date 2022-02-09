using Shard.Input;
using Shard.Patterns.Command;
using Shard.Patterns.Singleton;
using Shard.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shard.Controllers 
{
    public class EntityController : SingletonUnity
    {
        private static EntityController instance = null;
        public static new EntityController Instance { get { return (EntityController) instance; }}

        private GameObject player;

        private Command jumpButton;
        private Command moveButton;
        private Command reCallButton;
        private Command grabButton;
        private Command crouchButton;
        private Command programButton;

        private InputActions InputActions;
        private InputAction movement;

        private EntityMovement playerMovement;
        private EntityActions playerActions;

        private void Awake() {
            // Init the controller's instance
            instance = this;

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
            crouchButton = new CrouchCommand(playerMovement);
            reCallButton = new ActionCommand(playerActions);
            grabButton = new ActionCommand(playerActions);
            programButton = new ProgramCommand();
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

            InputActions.Player.Crouch.started += context => crouchButton.ExecuteWithParameters(true);
            InputActions.Player.Crouch.canceled += context => crouchButton.ExecuteWithParameters(false);
            InputActions.Player.Crouch.Enable();

            InputActions.Player.ReCall.performed += context => reCallButton.ExecuteWithParameters(EntityActions.Action.RECALL);
            InputActions.Player.ReCall.Enable();

            InputActions.Player.Grab.performed += context => grabButton.ExecuteWithParameters(EntityActions.Action.GRAB);
            InputActions.Player.Grab.Enable();

            InputActions.Player.Program.performed += context => programButton.Execute();
            InputActions.Player.Program.Enable();
        }

        private void OnDisable() {
            DisableInput();
        }

        private void DisableInput() {
            movement.Disable();

            InputActions.Player.Jump.Disable();
            InputActions.Player.Crouch.Disable();
            InputActions.Player.ReCall.Disable();
            InputActions.Player.Grab.Disable();
            InputActions.Player.Program.Disable();
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


