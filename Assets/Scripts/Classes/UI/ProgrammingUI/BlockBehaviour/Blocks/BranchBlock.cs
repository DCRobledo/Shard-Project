using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BranchBlock : BehaviourBlock
    {
        private int nextBlockIndex;


        private void Awake() {
            type = BlockType.BRANCH;
        }


        public override BlockLocation GetNextBlockLocation()
        {
            return new BlockLocation(nextBlockIndex, -1);
        }

        public void SetNextBlockIndex(int nextBlockIndex)
        {
            this.nextBlockIndex = nextBlockIndex;
        }
    }
}
