using Shard.Entities;
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

        private BlockBehaviour blockBehaviour;
        private Coroutine blockBehaviourExecution;

        private void Awake() {
            instance = this;

            robot = GameObject.Find("robot");

            robotMovement = robot.GetComponent<EntityMovement>();
            robotActions = robot.GetComponent<EntityActions>();
        }

        private void OnEnable() {
            BlockManagement.generateBlockBehaviourEvent += SetBlockBehaviour;
        }

        private void OnDisable() {
            BlockManagement.generateBlockBehaviourEvent -= SetBlockBehaviour;
        }



        private void SetBlockBehaviour(int maxIndex, List<GameObject> blocks) {
            if(blockBehaviour != null) Destroy(robot.GetComponent<BlockBehaviour>());

            blockBehaviour = robot.AddComponent<BlockBehaviour>();
            blockBehaviour.CreateBlockBehaviour(maxIndex, blocks);
            
            LinkActionBlocks();
            LinkConditionalBlocks();
        }

        private void LinkActionBlocks() {
            for (int i = 0; i < blockBehaviour.GetMaxIndex(); i++) {
                BehaviourBlock currentBlock = blockBehaviour.GetBlock(i);
                if (currentBlock.GetType() == BehaviourBlock.BlockType.ACTION) {
                    ActionBlock actionBlock = currentBlock as ActionBlock;

                    LinkRobotAction(ref actionBlock);
                }
            }
        }

        private void LinkRobotAction(ref ActionBlock block) {
            // switch (block.GetAction()) {
            //     case ActionBlock.BlockAction.WALK: block.executeActionEvent += robotMovement.Move; break;
            //     case ActionBlock.BlockAction.JUMP: block.executeActionEvent += robotMovement.Jump; break;
            //     case ActionBlock.BlockAction.FLIP: block.executeActionEvent += robotMovement.Flip; break;

            //     default: break;
            // }
        }

        private void LinkConditionalBlocks() {
            for (int i = 0; i < blockBehaviour.GetMaxIndex(); i++) {
                BehaviourBlock currentBlock = blockBehaviour.GetBlock(i);
                if (currentBlock.GetType() == BehaviourBlock.BlockType.CONDITIONAL) {
                    ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

                    LinkRobotSensor(ref conditionalBlock);
                }
            }
        }

        private void LinkRobotSensor(ref ConditionalBlock block) {

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


