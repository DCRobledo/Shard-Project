using Shard.Entities;
using Shard.Patterns.Command;
using Shard.Patterns.Singleton;
using Shard.UI.ProgrammingUI;
using Shard.Enums;
using System;
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

        private void Awake() {
            instance = this;

            robot = GameObject.Find("robot");

            robotMovement = robot.GetComponent<RobotMovement>();
            robotActions = robot.GetComponent<EntityActions>();
            robotSensors = robot.GetComponent<RobotSensors>();

            jumpCommand = new JumpCommand(robotMovement);
            moveCommand = new MoveCommand(robotMovement);
            flipCommand = new FlipCommand(robotMovement);
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

                if (currentBlock.GetType() == BlockEnum.BlockType.ACTION) {
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

                if (currentBlock.GetType() == BlockEnum.BlockType.CONDITIONAL) {
                    ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

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
            if(commandBehaviour != null) Destroy(robot.GetComponent<CommandBehaviour>());

            commandBehaviour = robot.AddComponent<CommandBehaviour>();
            commandBehaviour.CreateCommandBehaviour(commandEvent, commandTrigger, commandDelay);

            PlayerController.jumpTrigger += CommandBehaviour.commandTrigger.Invoke;
            CommandBehaviour.commandEvent += jumpCommand.Execute;
            // LinkCommandEvent();
            // LinkCommandTrigger();
        }

        // private void LinkCommandEvent() {
        //     switch(commandBehaviour.GetCommandEventAction()) {
        //         case CommandBehaviour.CommandAction.JUMP: PlayerMovement.jumpTrigger += jumpCommand.Execute; break;

        //         case CommandBehaviour.CommandAction.WALK:
        //             break;

        //         case CommandBehaviour.CommandAction.FLIP:
        //             break;
        //     }
        // }

        // private void LinkCommandTrigger() {
        //     switch (commandBehaviour.GetCommandTriggerInvoker()) {
        //         case CommandBehaviour.TriggerInvoker.LILY: LinkCommandTriggerToPlayer(); break;

        //         case CommandBehaviour.TriggerInvoker.ROBOT: LinkCommandTriggerToRobot(); break;
        //     }
        // }

        // private void LinkCommandTriggerToPlayer() {
        //     switch(commandBehaviour.GetCommandTriggerAction()) {
        //         case CommandBehaviour.CommandAction.JUMP: PlayerMovement.jumpTrigger += CommandBehaviour.commandTrigger.Invoke; break;

        //         case CommandBehaviour.CommandAction.WALK:
        //             break;

        //         case CommandBehaviour.CommandAction.FLIP:
        //             break;
        //     }
        // }

        // private void LinkCommandTriggerToRobot() {
        //     Action commandTrigger = null;

        //     switch(commandBehaviour.GetCommandTriggerAction()) {
        //         case CommandBehaviour.CommandAction.JUMP: /*commandTrigger = robotMovement.GetJumpTrigger();*/ break;

        //         case CommandBehaviour.CommandAction.WALK:
        //             break;

        //         case CommandBehaviour.CommandAction.FLIP:
        //             break;
        //     }

        //     //commandTrigger += commandBehaviour.GetCommandEvent().Invoke;
        // }

    
        public void TurnOn() {
            if(blockBehaviourExecution != null)
                StopCoroutine(blockBehaviourExecution);

            blockBehaviourExecution = StartCoroutine(blockBehaviour.ExecuteBehavior());
        }

        public void TurnOff() {
            if(blockBehaviourExecution != null)
                StopCoroutine(blockBehaviourExecution);
        }
    }
}


