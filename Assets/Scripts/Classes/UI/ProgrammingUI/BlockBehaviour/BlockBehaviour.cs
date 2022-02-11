using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockBehaviour
    {
        // The rows represent the block's index, and the columns, the block's indentation
        private BehaviourBlock[,] blocks;

        private int maxIndex;


        public BlockBehaviour(int maxIndex, List<GameObject> blocks) {
            this.blocks = new BehaviourBlock[maxIndex, 3];
            this.maxIndex = maxIndex;

            foreach(GameObject block in blocks) {
                BehaviourBlock behaviourBlock = block.GetComponent<BehaviourBlock>();

                this.blocks[behaviourBlock.GetIndex() - 1, behaviourBlock.GetIndentation() - 1] = behaviourBlock;
            }

            Print();
        }

        private BehaviourBlock GetBlock(int index, int indentation)
        {
            return blocks[index - 1, indentation - 1];
        }

        public void Print() {
            string message = "";

            for(int i = 1; i <= maxIndex; i++)
                for(int j = 1; j <= 3; j++)
                    if (GetBlock(i, j) != null)
                        message += "Block (" + GetBlock(i, j).GetIndex().ToString() + ", " + GetBlock(i, j).GetIndentation().ToString() + ") = " + GetBlock(i, j).ToString() + "\n";
                    
            Debug.Log(message);
        }
    }
}

