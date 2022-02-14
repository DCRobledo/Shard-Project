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

            blockBehaviour?.Print();
        }
    }
}


