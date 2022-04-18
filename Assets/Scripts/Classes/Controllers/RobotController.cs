using Shard.Entities;
using Shard.Patterns.Command;
using Shard.Patterns.Singleton;
using Shard.UI.ProgrammingUI;
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
        

        private void Awake() {
            instance = this;

            robot = GameObject.Find("robot");

            robotMovement = robot.GetComponent<RobotMovement>();
            robotActions = robot.GetComponent<RobotActions>();
            robotSensors = robot.GetComponent<RobotSensors>();

            jumpCommand = new JumpCommand(robotMovement);
            moveCommand = new MoveCommand(robotMovement);
            flipCommand = new FlipCommand(robotMovement);

            gravityScale = robot.GetComponent<Rigidbody2D>().gravityScale;
            mass = robot.GetComponent<Rigidbody2D>().mass;
            angularDrag = robot.GetComponent<Rigidbody2D>().angularDrag;
        }

        private void OnEnable() {
            BlockManagement.generateBlockBehaviourEvent += SetBlockBehaviour;
            InputConsole.generateCommandBehaviourEvent  += SetCommandBehaviour;
        }

        private void OnDisable() {
            BlockManagement.generateBlockBehaviourEvent -= SetBlockBehaviour;
            InputConsole.generateCommandBehaviourEvent  -= SetCommandBehaviour;
        }


        private void SetBlockBehaviour(int minIndex, int maxIndex, List<GameObject> blocks) {
            if(blockBehaviour != null) Destroy(robot.GetComponent<BlockBehaviour>());

            blockBehaviour = robot.AddComponent<BlockBehaviour>();
            blockBehaviour.CreateBlockBehaviour(minIndex, maxIndex, blocks);
            
            LinkActionBlocks();
            LinkConditionalBlocks();
        }

        private void LinkActionBlocks() {
            for (int i = blockBehaviour.GetMinIndex() + 1; i < blockBehaviour.GetMaxIndex() + 1; i++) {
                BehaviourBlock currentBlock = blockBehaviour.GetBlock(i);

                if (currentBlock?.GetType() == BlockEnum.BlockType.ACTION) {
                    ActionBlock actionBlock = currentBlock as ActionBlock;

                    LinkRobotAction(ref actionBlock);
                }
            }
        }

        private void LinkRobotAction(ref ActionBlock block) {
            switch (block.GetAction()) {
                case EntityEnum.Action.MOVE: block.executeActionEvent += moveCommand.Execute ; break;
                case EntityEnum.Action.JUMP: block.executeActionEvent += jumpCommand.Execute; break;
                case EntityEnum.Action.FLIP: block.executeActionEvent += flipCommand.Execute; break;

                default: break;
            }
        }

        private void LinkConditionalBlocks() {
            for (int i = blockBehaviour.GetMinIndex() + 1; i < blockBehaviour.GetMaxIndex() + 1; i++) {
                BehaviourBlock currentBlock = blockBehaviour.GetBlock(i);

                if (currentBlock?.GetType() == BlockEnum.BlockType.CONDITIONAL) {
                    ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

                    if (conditionalBlock.GetConditionalType() != BlockEnum.ConditionalType.ELSE)
                        LinkRobotSensor(ref conditionalBlock);
                }
            }
        }

        private void LinkRobotSensor(ref ConditionalBlock block) {
            // Remove previous events
            block.GetCondition().isMetEvent = null;

            switch(block.GetCondition().GetState()) {
                case BlockEnum.ConditionalState.AHEAD:  block.GetCondition().isMetEvent += robotSensors.CheckAhead;  break;
                case BlockEnum.ConditionalState.BEHIND: block.GetCondition().isMetEvent += robotSensors.CheckBehind; break;
                case BlockEnum.ConditionalState.ABOVE:  block.GetCondition().isMetEvent += robotSensors.CheckAbove;  break;
                case BlockEnum.ConditionalState.BELOW:  block.GetCondition().isMetEvent += robotSensors.CheckBelow;  break;

                default: break;
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
            UnlinkCommandTrigger();

            commandBehaviour = null;

            Destroy(robot.GetComponent<CommandBehaviour>());
        }

        private void LinkCommandEvent() {
            switch(commandBehaviour.GetCommandEventAction()) {
                case EntityEnum.Action.JUMP:     CommandBehaviour.commandEvent += jumpCommand.Execute; break;
                case EntityEnum.Action.MOVE:     CommandBehaviour.commandEvent += moveCommand.Execute; break;
                case EntityEnum.Action.FLIP:     CommandBehaviour.commandEvent += flipCommand.Execute; break;
            }
        }

        private void LinkCommandTrigger() {
            switch (commandBehaviour.GetCommandTriggerInvoker()) {
                case CommandEnum.TriggerInvoker.LILY: LinkCommandTriggerToPlayer(); break;
                case CommandEnum.TriggerInvoker.ROBOT: LinkCommandTriggerToRobot(); break;
            }
        }

        private void LinkCommandTriggerToPlayer() {
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

            // Update rigidbody2D parameters

            robot.GetComponent<Rigidbody2D>().gravityScale = isRobotGrabbed ? 0f : gravityScale;
            robot.GetComponent<Rigidbody2D>().mass         = isRobotGrabbed ? 0f : mass; 
            robot.GetComponent<Rigidbody2D>().angularDrag  = isRobotGrabbed ? 0f : angularDrag;  
        }

        public bool IsRobotOn() { return this.isRobotOn; }
        public bool IsRobotGrabbed() { return this.isRobotGrabbed; }
    }
}


