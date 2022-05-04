using Shard.Entities;
using Shard.Patterns.Command;
using Shard.Patterns.Singleton;
using Shard.UI.ProgrammingUI;
using Shard.Gameflow;
using Shard.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Controllers
{
    public class RobotController : SingletonUnity
    {
        private static RobotController instance = null;
        public static new RobotController Instance { get { return (RobotController) instance; }}

        private GameObject robot;
        private EntityMovement robotMovement;
        private EntityActions robotActions;
        private RobotSensors robotSensors;

        private Command jumpCommand;
        private Command moveCommand;
        private Command flipCommand;

        private BlockBehaviour blockBehaviour;
        private Coroutine blockBehaviourExecution;

        private CommandBehaviour commandBehaviour;

        private bool isRobotOn = false;
        private bool isRobotGrabbed = false;

        private float gravityScale;
        private float mass;
        private float angularDrag;

        public static Action turnOnTrigger;
        public static Action turnOffTrigger;
        

        private void Awake() {
            // Initialize the controller's instance
            instance = this;
        }

        private void Start() {
            // Get the robot and its components
            robot = GameObject.Find("robot");

            robotMovement = robot.GetComponent<RobotMovement>();
            robotActions = robot.GetComponent<RobotActions>();
            robotSensors = robot.GetComponent<RobotSensors>();

            // Initialize the commands
            jumpCommand = new JumpCommand(robotMovement);
            moveCommand = new MoveCommand(robotMovement);
            flipCommand = new FlipCommand(robotMovement);

            // Store the rigidbody's init values
            gravityScale = robot.GetComponent<Rigidbody2D>().gravityScale;
            mass = robot.GetComponent<Rigidbody2D>().mass;
            angularDrag = robot.GetComponent<Rigidbody2D>().angularDrag;
        }

        private void OnEnable() {
            BlockManagement.generateBlockBehaviourEvent += SetBlockBehaviour;
            InputConsole.generateCommandBehaviourEvent  += SetCommandBehaviour;

            GameFlowManagement.sceneChangeEvent += ResetBlockBehaviour;
            GameFlowManagement.sceneChangeEvent += ResetCommandBehaviour;
        }

        private void OnDisable() {
            BlockManagement.generateBlockBehaviourEvent -= SetBlockBehaviour;
            InputConsole.generateCommandBehaviourEvent  -= SetCommandBehaviour;

            GameFlowManagement.sceneChangeEvent -= ResetBlockBehaviour;
            GameFlowManagement.sceneChangeEvent -= ResetCommandBehaviour;
        }


        private void SetBlockBehaviour(int minIndex, int maxIndex, List<GameObject> blocks) {
            if(blockBehaviour != null) ResetBlockBehaviour();

            blockBehaviour = robot.AddComponent<BlockBehaviour>();
            blockBehaviour.CreateBlockBehaviour(minIndex, maxIndex, blocks);
            
            LinkActionBlocks();
            LinkConditionalBlocks();
        }

        private void LinkActionBlocks(bool link = true) {
            // Look for all action blocks
            for (int i = blockBehaviour.GetMinIndex() + 1; i < blockBehaviour.GetMaxIndex() + 1; i++) {
                BehaviourBlock currentBlock = blockBehaviour.GetBlock(i);

                if (currentBlock?.GetType() == BlockEnum.BlockType.ACTION) {
                    ActionBlock actionBlock = currentBlock as ActionBlock;

                    LinkRobotAction(ref actionBlock, link);
                }
            }
        }

        private void LinkRobotAction(ref ActionBlock block, bool link = true) {
            // Link or unlink the execution of the block to the execution of its corresponding command
            if(!link) block.executeActionEvent = null;

            else {
                switch (block.GetAction()) {
                    case EntityEnum.Action.MOVE: block.executeActionEvent += moveCommand.Execute ; break;
                    case EntityEnum.Action.JUMP: block.executeActionEvent += jumpCommand.Execute; break;
                    case EntityEnum.Action.FLIP: block.executeActionEvent += flipCommand.Execute; break;

                    default: break;
                }
            }
        }

        private void LinkConditionalBlocks(bool link = true) {
            // Look for all conditional blocks
            for (int i = blockBehaviour.GetMinIndex() + 1; i < blockBehaviour.GetMaxIndex() + 1; i++) {
                BehaviourBlock currentBlock = blockBehaviour.GetBlock(i);

                if (currentBlock?.GetType() == BlockEnum.BlockType.CONDITIONAL) {
                    ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

                    if (conditionalBlock.GetConditionalType() != BlockEnum.ConditionalType.ELSE)
                        LinkRobotSensor(ref conditionalBlock, link);
                }
            }
        }

        private void LinkRobotSensor(ref ConditionalBlock block, bool link = true) {
            // Link or unlink the block's condition to the robot's sensor
            if(!link) block.GetCondition().isMetEvent = null;

            else {
                switch(block.GetCondition().GetState()) {
                    case BlockEnum.ConditionalState.AHEAD:  block.GetCondition().isMetEvent += robotSensors.CheckAhead;  break;
                    case BlockEnum.ConditionalState.BEHIND: block.GetCondition().isMetEvent += robotSensors.CheckBehind; break;
                    case BlockEnum.ConditionalState.ABOVE:  block.GetCondition().isMetEvent += robotSensors.CheckAbove;  break;
                    case BlockEnum.ConditionalState.BELOW:  block.GetCondition().isMetEvent += robotSensors.CheckBelow;  break;

                    default: break;
                }
            }
            
        }

        public void ResetBlockBehaviour() {
            StopBlockBehaviour();

            if(blockBehaviour != null) {

                LinkActionBlocks(false);
                LinkConditionalBlocks(false);

                blockBehaviour = null;

                Destroy(robot.GetComponent<BlockBehaviour>());
            }
        }


        private void SetCommandBehaviour(string commandEvent, string commandTrigger, string commandDelay) {
            if(commandBehaviour != null) ResetCommandBehaviour();

            commandBehaviour = robot.AddComponent<CommandBehaviour>();
            commandBehaviour.CreateCommandBehaviour(commandEvent, commandTrigger, commandDelay);

            LinkCommandEvent();
            LinkCommandTrigger();
        }

        public void ResetCommandBehaviour() {
            if(commandBehaviour != null) {
                UnlinkCommandTrigger();

                commandBehaviour = null;

                Destroy(robot.GetComponent<CommandBehaviour>());
            }
        }

        private void LinkCommandEvent() {
            // Link the command's event to the corresponding robot's command
            switch(commandBehaviour.GetCommandEventAction()) {
                case EntityEnum.Action.JUMP:     CommandBehaviour.commandEvent += jumpCommand.Execute; break;
                case EntityEnum.Action.MOVE:     CommandBehaviour.commandEvent += moveCommand.Execute; break;
                case EntityEnum.Action.FLIP:     CommandBehaviour.commandEvent += flipCommand.Execute; break;
            }
        }

        private void LinkCommandTrigger() {
            // Link the trigger to the corresponding entity
            switch (commandBehaviour.GetCommandTriggerInvoker()) {
                case CommandEnum.TriggerInvoker.LILY: LinkCommandTriggerToPlayer(); break;
                case CommandEnum.TriggerInvoker.ROBOT: LinkCommandTriggerToRobot(); break;
            }
        }

        private void LinkCommandTriggerToPlayer() {
            // Link the trigger to a player action
            switch(commandBehaviour.GetCommandTriggerAction()) {
                case EntityEnum.Action.JUMP:   PlayerController.jumpTrigger += CommandBehaviour.commandTrigger.Invoke; break;
                case EntityEnum.Action.MOVE:   PlayerController.moveTrigger += CommandBehaviour.commandTrigger.Invoke; 
                                               PlayerController.stopTrigger += commandBehaviour.StopAllCoroutines;     break;

                case EntityEnum.Action.FLIP:   PlayerMovement.flipTrigger   += CommandBehaviour.commandTrigger.Invoke; break;
                case EntityEnum.Action.CROUCH: PlayerMovement.crouchTrigger += CommandBehaviour.commandTrigger.Invoke; break;

                case EntityEnum.Action.GRAB:   PlayerActions.grabTrigger    += CommandBehaviour.commandTrigger.Invoke; break;
                case EntityEnum.Action.DROP:   PlayerActions.dropTrigger    += CommandBehaviour.commandTrigger.Invoke; break;
            }
        }

        private void LinkCommandTriggerToRobot() {
            // Link the trigger to a robot action
            switch(commandBehaviour.GetCommandTriggerAction()) {
                case EntityEnum.Action.JUMP:    robotMovement.SubscribeToJumpTrigger(CommandBehaviour.commandTrigger); break;

                case EntityEnum.Action.MOVE:    RobotMovement.moveTrigger     += CommandBehaviour.commandTrigger.Invoke; break;
                case EntityEnum.Action.FLIP:    RobotMovement.flipTrigger     += CommandBehaviour.commandTrigger.Invoke; break;

                case EntityEnum.Action.TURN_ON: RobotController.turnOnTrigger += CommandBehaviour.commandTrigger.Invoke; break;
            }
        }

        private void UnlinkCommandTrigger() {
            PlayerController.jumpTrigger  -= CommandBehaviour.commandTrigger.Invoke;
            PlayerController.moveTrigger  -= CommandBehaviour.commandTrigger.Invoke;
            PlayerController.stopTrigger  -= commandBehaviour.StopAllCoroutines;

            PlayerMovement.flipTrigger    -= CommandBehaviour.commandTrigger.Invoke;
            PlayerMovement.crouchTrigger  -= CommandBehaviour.commandTrigger.Invoke;

            PlayerActions.grabTrigger     -= CommandBehaviour.commandTrigger.Invoke;
            PlayerActions.dropTrigger     -= CommandBehaviour.commandTrigger.Invoke;

            robotMovement.UnsubscribeFromJumpTrigger(CommandBehaviour.commandTrigger);

            RobotMovement.moveTrigger     -= CommandBehaviour.commandTrigger.Invoke;
            RobotMovement.flipTrigger     -= CommandBehaviour.commandTrigger.Invoke;

            RobotController.turnOnTrigger -= CommandBehaviour.commandTrigger.Invoke;
        }

    
        public void TurnOn() {
            if(!isRobotOn) {
                isRobotOn = true;

                turnOnTrigger?.Invoke();
            }
        }

        public void TurnOff() {
            if(isRobotOn) {
                isRobotOn = false;

                turnOffTrigger?.Invoke();

                if(blockBehaviourExecution != null)
                    StopCoroutine(blockBehaviourExecution);
            }
        }

        public void StartBlockBehaviour() {
            if(isRobotOn) {
                if(blockBehaviourExecution != null)
                    StopCoroutine(blockBehaviourExecution);

                if(blockBehaviour != null && !blockBehaviour.IsEmpty())
                    blockBehaviourExecution = StartCoroutine(blockBehaviour.ExecuteBehavior());
            }
        }

        public void StopBlockBehaviour() {
            if(blockBehaviourExecution != null)
                StopCoroutine(blockBehaviourExecution);
        }


        public void ToggleIsRobotGrabbed() {
            // Update control variable
            this.isRobotGrabbed = !this.isRobotGrabbed;

            // Update polygonCollider2D
            robot.GetComponent<PolygonCollider2D>().enabled = !isRobotGrabbed;
            robot.transform.GetChild(0).gameObject.SetActive(!isRobotGrabbed);

            // Update rigidbody2D parameters
            Rigidbody2D rigidbody2D = robot.GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = isRobotGrabbed ? 0f : gravityScale;
            rigidbody2D.mass         = isRobotGrabbed ? 0f : mass; 
            rigidbody2D.angularDrag  = isRobotGrabbed ? 0f : angularDrag;  
        }

        public bool IsRobotOn() { return this.isRobotOn; }
        public bool IsRobotGrabbed() { return this.isRobotGrabbed; }
    }
}


