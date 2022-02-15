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
            blockBehaviour.Print();
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


