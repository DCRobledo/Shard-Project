using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BranchBlock : BehaviourBlock
    {
        public int nextBlockIndex;


        private void Awake() {
            type = BlockType.BRANCH;

            nextBlockIndex = 1;
        }

        public override BlockLocation GetNextBlockLocation()
        {
            return new BlockLocation(nextBlockIndex, -1);
        }

        public void SetNextBlockIndex(int nextBlockIndex)
        {
            this.nextBlockIndex = nextBlockIndex + 1;
        }
    }
}
