using Shard.Input;
using Shard.Patterns.Command;
using Shard.Patterns.Singleton;
using Shard.Entities;
using Shard.UI.ProgrammingUI;
using Shard.Enums;
using System;
using System.Collections;
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

        private Coroutine checkForStop;

        private Vector2 movingDirection;
        
        private GameObject player;
        private EntityMovement playerMovement;
        private EntityActions playerActions;
        private PlayerAnimations playerAnimations;

        private Command jumpButton;
        private Command moveButton;
        private Command reCallButton;
        private Command grabButton;
        private Command crouchButton;
        private Command programButton;
        private Command returnToCheckpointButton;
        private Command pauseButton;

        private InputActions InputActions;
        private InputAction movement;

        public static Action stopTrigger;
        public static Action moveTrigger;
        public static Action jumpTrigger;


        private void Awake() {
            // Init the controller's instance
            instance = this;

            // Initialize the input system
            InputActions = new InputActions();
        }

        private void Start() {
            // Get the player game object and its components
            player = GameObject.Find("player");

            playerMovement = player.GetComponent<PlayerMovement>();
            playerActions = player.GetComponent<PlayerActions>();
            playerAnimations = player.GetComponent<PlayerAnimations>();

            AwakeInput();
        }

        private void AwakeInput() {
            jumpButton = new JumpCommand(playerMovement);
            moveButton = new MoveCommand(playerMovement);
            crouchButton = new CrouchCommand(playerMovement);
            reCallButton = new ActionCommand(playerActions);
            grabButton = new ActionCommand(playerActions);
            programButton = new ProgramCommand();
            returnToCheckpointButton = new ReturnToCheckpointCommand(EntityEnum.Entity.PLAYER);
            pauseButton = new PauseCommand();
        }

        private void OnEnable() {
            // Subscribe actions and movement to input
            movement = InputActions.Player.Movement;

            InputActions.Player.Jump.started += context => jumpButton.ExecuteWithParameters(true);
            InputActions.Player.Jump.canceled += context => jumpButton.ExecuteWithParameters(false);

            InputActions.Player.Drop.started += context => playerMovement.SetIsPressingDown(true);
            InputActions.Player.Drop.canceled += context => playerMovement.SetIsPressingDown(false);

            InputActions.Player.Crouch.started += context => crouchButton.ExecuteWithParameters(true);
            InputActions.Player.Crouch.canceled += context => crouchButton.ExecuteWithParameters(false);

            InputActions.Player.ReCall.performed += context => reCallButton.ExecuteWithParameters(EntityEnum.Action.RECALL);
            InputActions.Player.Grab.performed += context => grabButton.ExecuteWithParameters(EntityEnum.Action.GRAB);

            InputActions.Player.Program.performed += context => programButton.Execute();

            InputActions.Player.ReturnToCheckpoint.performed += context => returnToCheckpointButton.Execute();

            InputActions.Player.Pause.performed += context => pauseButton.Execute();

            // Subscribe enabling and disabling to input console state management
            InputConsole.enterInputStateEvent += DisableInput;
            InputConsole.exitInputStateEvent += EnableInput;
            
            if(selfControlled) EnableInput();
            EnableTriggers();
        }

        public void EnableInput() {
            movement.Enable();

            InputActions.Player.Jump.Enable();
            InputActions.Player.Drop.Enable();
            InputActions.Player.Crouch.Enable();
            InputActions.Player.ReCall.Enable();
            InputActions.Player.Grab.Enable();
            InputActions.Player.Program.Enable();
            InputActions.Player.ReturnToCheckpoint.Enable();
            InputActions.Player.Pause.Enable();
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
            InputActions.Player.Drop.Disable();
            InputActions.Player.Crouch.Disable();
            InputActions.Player.ReCall.Disable();
            InputActions.Player.Grab.Disable();
            InputActions.Player.Program.Disable();
            InputActions.Player.ReturnToCheckpoint.Disable();
            InputActions.Player.Pause.Disable();
        }


        private void FixedUpdate() {
            // Move player based on input direction
            movingDirection = movement.ReadValue<Vector2>();
            MovePlayer();
        }


        private void InvokeTrigger(EntityEnum.Action trigger) {
            switch(trigger) {
                case EntityEnum.Action.STOP:   stopTrigger?.Invoke();  break;
                case EntityEnum.Action.MOVE:   moveTrigger?.Invoke();  break;
                case EntityEnum.Action.JUMP:   jumpTrigger?.Invoke();  break;
            }
        }

        private void MovePlayer() {
            object[] parameters = {movingDirection.x, movingDirection.y};

            if((float) parameters[0] != 0) {
                isMoving = true;
                
                InvokeTrigger(EntityEnum.Action.MOVE);
            } 

            // If no input is registered, then we check for a possible stop
            else if(isMoving) 
            {
                isMoving = false;
                checkForStop = StartCoroutine(CheckForStop());
            }

            moveButton.ExecuteWithParameters(parameters);
        }

        private IEnumerator CheckForStop() {
            // Wait for a couple of frames to know if it is a full stop or just a change in the moving direction
            for(int i = 0; i < 10 && movingDirection.x == 0; i++)
                yield return new WaitForFixedUpdate();
            
            if(movingDirection.x == 0) 
                InvokeTrigger(EntityEnum.Action.STOP);
        }

        public void ReleaseRobot() { 
            (playerActions as PlayerActions).ReleaseRobot();

            playerAnimations.SetGrabTrigger();
        }

        public void ExpandPlayerCollider(bool expand) {
            // Whenever the player grabs the robot, we expand its collider to match the new sprite combination
            PolygonCollider2D polygonCollider2D = player.GetComponent<PolygonCollider2D>();

            Vector2[] points = polygonCollider2D.points;
            float polygonModifier = expand ? 2f : .5f;
            points[4].x *= polygonModifier;

            points[3] = expand ? new Vector2(.72f, -.2f) : new Vector2(.36f, -.6f);
            

            polygonCollider2D.SetPath(0, points);
        }
    }
}


