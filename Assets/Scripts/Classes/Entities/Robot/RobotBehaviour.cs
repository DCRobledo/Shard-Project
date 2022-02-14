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


        private void SetBlockBehaviour(BlockBehaviour blockBehaviour) {
            this.blockBehaviour = blockBehaviour;
        }
    
    
        public void TurnOn() {
            if(behaviourExecution != null)
                StopCoroutine(behaviourExecution);

            behaviourExecution = StartCoroutine(ExecuteBehaviour());
        }

        public void TurnOff() {
            if(behaviourExecution != null)
                StopCoroutine(behaviourExecution);
        }

        private IEnumerator ExecuteBehaviour() {
            blockBehaviour.ExecuteBehavior();

            yield return null;
        }
    }
}


