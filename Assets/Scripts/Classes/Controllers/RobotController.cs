using Shard.Entities;
using Shard.Patterns.Command;
using Shard.Patterns.Singleton;
using Shard.UI.ProgrammingUI;
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

            robotMovement = robot.GetComponent<EntityMovement>();
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

                if (currentBlock.GetType() == BehaviourBlock.BlockType.ACTION) {
                    ActionBlock actionBlock = currentBlock as ActionBlock;

                    LinkRobotAction(ref actionBlock);
                }
            }
        }

        private void LinkRobotAction(ref ActionBlock block) {
            switch (block.GetAction()) {
                case ActionBlock.BlockAction.WALK: block.executeActionEvent += moveCommand.Execute ; break;
                case ActionBlock.BlockAction.JUMP: block.executeActionEvent += jumpCommand.Execute; break;
                case ActionBlock.BlockAction.FLIP: block.executeActionEvent += flipCommand.Execute; break;

                default: break;
            }
        }

        private void LinkConditionalBlocks() {
            for (int i = blockBehaviour.GetMinIndex() + 1; i < blockBehaviour.GetMaxIndex() + 1; i++) {
                BehaviourBlock currentBlock = blockBehaviour.GetBlock(i);

                if (currentBlock.GetType() == BehaviourBlock.BlockType.CONDITIONAL) {
                    ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

                    LinkRobotSensor(ref conditionalBlock);
                }
            }
        }

        private void LinkRobotSensor(ref ConditionalBlock block) {
            // Remove previous events
            block.GetCondition().isMetEvent = null;

            switch(block.GetCondition().GetState()) {
                case Condition.ConditionalState.AHEAD:  block.GetCondition().isMetEvent += robotSensors.CheckAhead;  break;
                case Condition.ConditionalState.BEHIND: block.GetCondition().isMetEvent += robotSensors.CheckBehind; break;
                case Condition.ConditionalState.ABOVE:  block.GetCondition().isMetEvent += robotSensors.CheckAbove;  break;
                case Condition.ConditionalState.BELOW:  block.GetCondition().isMetEvent += robotSensors.CheckBelow;  break;

                default: break;
            }
        }


        private void SetCommandBehaviour(string commandEvent, string commandTrigger, string commandDelay) {
            if(commandBehaviour != null) Destroy(robot.GetComponent<CommandBehaviour>());

            commandBehaviour = robot.AddComponent<CommandBehaviour>();
            commandBehaviour.CreateCommandBehaviour(commandEvent, commandTrigger, commandDelay);
        }

    
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


