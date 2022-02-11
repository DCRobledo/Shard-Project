using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockBehaviour
    {
        // The rows represent the block's index, and the columns, the block's indentation
        private BehaviourBlock[,] blocks;

        public BlockBehaviour(List<GameObject> blocks) {
            this.blocks = new BehaviourBlock[blocks.Count, 3];

            foreach(GameObject block in blocks) {
                BehaviourBlock behaviourBlock = block.GetComponent<BehaviourBlock>();
                Debug.Log(behaviourBlock);

                this.blocks[behaviourBlock.GetIndex(), behaviourBlock.GetIndentation()] = behaviourBlock;
            }
                

            Print();
        }

        public void Print() {
            string message = "";

            for(int i = 0; i < blocks.Length; i++)
                for(int j = 0; j < blocks.GetLength(i); j++)
                    if (blocks[i,j] != null)
                        message += "Block (" + blocks[i,j].GetIndex().ToString() + ", " + blocks[i,j].GetIndentation().ToString() + ") = " + blocks[i,j].GetType().ToString();

            Debug.Log(message);
        }
    }
}

