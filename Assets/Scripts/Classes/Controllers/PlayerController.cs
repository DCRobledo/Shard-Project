using Shard.Input;
using Shard.Patterns.Command;
using Shard.Patterns.Singleton;
using Shard.Entities;
using Shard.UI.ProgrammingUI;
using Shard.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shard.Controllers 
{
    public class PlayerController : SingletonUnity
    {
        private static PlayerController instance = null;
        public static new PlayerController Instance { get { return (PlayerController) instance; }}

        [SerializeField]
        private bool selfControlled = false;
        private bool isMoving = false;

        private GameObject player;
        private EntityMovement playerMovement;
        private EntityActions playerActions;

        private Command jumpButton;
        private Command moveButton;
        private Command reCallButton;
        private Command grabButton;
        private Command crouchButton;
        private Command programButton;

        private InputActions InputActions;
        private InputAction movement;

        public static Action stopTrigger;
        public static Action moveTrigger;
        public static Action jumpTrigger;
        


        private void Awake() {
            // Init the controller's instance
            instance = this;

            // Get the player game object and its components
            player = GameObject.Find("player");

            playerMovement = player.GetComponent<PlayerMovement>();
            playerActions = player.GetComponent<PlayerActions>();

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
            // Subscribe actions and movement to input
            movement = InputActions.Player.Movement;

            InputActions.Player.Jump.started += context => jumpButton.ExecuteWithParameters(true);
            InputActions.Player.Jump.canceled += context => jumpButton.ExecuteWithParameters(false);

            InputActions.Player.Crouch.started += context => crouchButton.ExecuteWithParameters(true);
            InputActions.Player.Crouch.canceled += context => crouchButton.ExecuteWithParameters(false);

            InputActions.Player.ReCall.performed += context => reCallButton.ExecuteWithParameters(EntityEnum.Action.RECALL);
            InputActions.Player.Grab.performed += context => grabButton.ExecuteWithParameters(EntityEnum.Action.GRAB);

            InputActions.Player.Program.performed += context => programButton.Execute();

            // Subscribe enabling and disabling to input console state management
            InputConsole.enterInputStateEvent += DisableInput;
            InputConsole.exitInputStateEvent += EnableInput;

            
            if(selfControlled) EnableInput();
            EnableTriggers();
        }

        public void EnableInput() {
            movement.Enable();

            InputActions.Player.Jump.Enable();
            InputActions.Player.Crouch.Enable();
            InputActions.Player.ReCall.Enable();
            InputActions.Player.Grab.Enable();
            InputActions.Player.Program.Enable();
        }

        private void EnableTriggers() {
            InputActions.Player.Jump.started += context => InvokeTrigger(EntityEnum.Action.JUMP);
        }

        private void OnDisable() {
            DisableInput();

            // Unsubscribe enabling and disabling to input console state management
            InputConsole.enterInputStateEvent -= DisableInput;
            InputConsole.exitInputStateEvent -= EnableInput;
        }

        public void DisableInput() {
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


        private void InvokeTrigger(EntityEnum.Action trigger) {
            switch(trigger) {
                case EntityEnum.Action.STOP: stopTrigger?.Invoke(); break;
                case EntityEnum.Action.MOVE: moveTrigger?.Invoke(); break;
                case EntityEnum.Action.JUMP: jumpTrigger?.Invoke(); break;
            }
        }

        private void MovePlayer(Vector2 direction) {
            object[] parameters = {direction.x, direction.y};

            if((float) parameters[0] != 0) {
                isMoving = true;
                
                InvokeTrigger(EntityEnum.Action.MOVE);
            } 

            else if(isMoving) {
                isMoving = false;

                InvokeTrigger(EntityEnum.Action.STOP);
            }                         
            
            moveButton.ExecuteWithParameters(parameters);
        }
    }
}


