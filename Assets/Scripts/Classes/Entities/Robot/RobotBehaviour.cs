using Shard.UI.ProgrammingUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class RobotBehaviour : MonoBehaviour
    {
        private BlockBehaviour blockBehaviour;


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
            StartCoroutine(ExecuteBehaviour());
        }

        public void TurnOff() {
            StopCoroutine(ExecuteBehaviour());
        }

        private IEnumerator ExecuteBehaviour() {
            blockBehaviour.ExecuteBehavior();

            yield return null;
        }

        // private IEnumerator ExecuteBehaviour() {
        //     // Execute the first block
        //     BehaviourBlock currentBlock = blockBehaviour.GetBlock();

        //     BlockLocation nextBlockLocation = currentBlock.Execute(); 

        //     // Now, we go through each block in the behaviour until we reach the end of it
        //     while(nextBlockLocation.GetIndex() < blockBehaviour.GetMaxIndex()) {
        //         Debug.Log("hey");
        //         if(currentBlock != null) {
        //             Debug.Log("Execute -> " + currentBlock.GetBlockLocation().ToString());

        //             // Execute the block and get the next block location
        //             nextBlockLocation = currentBlock.Execute();   

        //             // Get the next block
        //             currentBlock = blockBehaviour.GetBlock(nextBlockLocation.GetIndex(), nextBlockLocation.GetIndentation());
        //         }
        //     }

        //     yield return null;
        // }
    }
}


