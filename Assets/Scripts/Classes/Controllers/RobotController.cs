using Shard.Entities;
using Shard.Patterns.Command;
using Shard.Patterns.Singleton;
using Shard.UI.ProgrammingUI;
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

        [SerializeField]
        private LayerMask sensorDetectionLayer;

        private Command jumpCommand;
        private Command moveCommand;
        private Command flipCommand;

        private BlockBehaviour blockBehaviour;
        private Coroutine blockBehaviourExecution;

        private void Awake() {
            instance = this;

            robot = GameObject.Find("robot");

            robotMovement = robot.GetComponent<EntityMovement>();
            robotActions = robot.GetComponent<EntityActions>();

            RobotSensors.SetBoxCollider2D(robot.GetComponent<BoxCollider2D>());
            RobotSensors.SetLayerMask(sensorDetectionLayer);

            jumpCommand = new JumpCommand(robotMovement);
            moveCommand = new MoveCommand(robotMovement);
            flipCommand = new FlipCommand(robotMovement);
        }

        private void OnEnable() {
            BlockManagement.generateBlockBehaviourEvent += SetBlockBehaviour;
        }

        private void OnDisable() {
            BlockManagement.generateBlockBehaviourEvent -= SetBlockBehaviour;
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
            switch(block.GetCondition().GetState()) {
                case Condition.ConditionalState.AHEAD:  block.GetCondition().isMetEvent += RobotSensors.CheckAhead;  break;
                case Condition.ConditionalState.BEHIND: block.GetCondition().isMetEvent += RobotSensors.CheckBehind; break;
                case Condition.ConditionalState.ABOVE:  block.GetCondition().isMetEvent += RobotSensors.CheckAbove;  break;
                case Condition.ConditionalState.BELOW:  block.GetCondition().isMetEvent += RobotSensors.CheckBelow;  break;

                default: break;
            }
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


