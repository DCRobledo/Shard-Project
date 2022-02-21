using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shard.Enums;

namespace Shard.UI.ProgrammingUI
{
    public class BranchBlock : BehaviourBlock
    {
        public int nextBlockIndex;


        private void Awake() {
            type = BlockEnum.BlockType.BRANCH;

            nextBlockIndex = 1;
        }

        public override BlockLocation Execute()
        {
            return new BlockLocation(nextBlockIndex, -1);
        }

        public override string ToString() {
            return "BRANCH            -> GO TO " + nextBlockIndex.ToString();
        }


        public void SetNextBlockIndex(int nextBlockIndex)
        {
            this.nextBlockIndex = nextBlockIndex + 1;
        }
    }
}
