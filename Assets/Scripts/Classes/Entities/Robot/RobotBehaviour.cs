using Shard.UI.ProgrammingUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class RobotBehaviour : MonoBehaviour
    {
        private BlockBehaviour blockBehaviour;

        private Coroutine behaviourExecution;


        private void OnEnable() {
            BlockManagement.generateBlockBehaviourEvent += SetBlockBehaviour;
        }

        private void OnDisable() {
            BlockManagement.generateBlockBehaviourEvent -= SetBlockBehaviour;
        }


        private void SetBlockBehaviour(int maxIndex, List<GameObject> blocks) {
            this.blockBehaviour = this.gameObject.AddComponent<BlockBehaviour>();
            this.blockBehaviour.CreateBlockBehaviour(maxIndex, blocks);
        }
    
    
        public void TurnOn() {
            if(behaviourExecution != null)
                StopCoroutine(behaviourExecution);

            behaviourExecution = StartCoroutine(blockBehaviour.ExecuteBehavior());
        }

        public void TurnOff() {
            if(behaviourExecution != null)
                StopCoroutine(behaviourExecution);
        }
    }
}


