using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockBehaviour : MonoBehaviour
    {
        // The rows represent the block's index, and the columns the block's indentation
        private BehaviourBlock[,] blocks;

        private int maxIndex;


        public void CreateBlockBehaviour (int maxIndex, List<GameObject> blocks) {
            this.blocks = new BehaviourBlock[maxIndex, 3];
            this.maxIndex = maxIndex;

            foreach(GameObject block in blocks) {
                BehaviourBlock behaviourBlock = block.GetComponent<BehaviourBlock>();

                this.blocks[behaviourBlock.GetIndex() - 1, behaviourBlock.GetIndentation() - 1] = behaviourBlock;
            }

            AssignConditionalInformation();
            //CreateSubBehaviours();
        }

        private void AssignConditionalInformation() {
            // Look for all IF or ELSE_IF blocks
            for (int i = 1; i <= maxIndex; i++) {
                for(int j = 1; j <= 3; j++) {

                    BehaviourBlock currentBlock = GetBlock(i, j);
                    if(currentBlock != null) {

                        if(currentBlock.GetType() == BehaviourBlock.BlockType.CONDITIONAL) {
                            ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

                            // Link them with an ELSE block, if it exists and set their indexes
                            if(conditionalBlock.GetConditionalType() != ConditionalBlock.ConditionalType.ELSE) {
                                LookForElseBlock(ref conditionalBlock);
                                LookForEndOfConditional(ref conditionalBlock);
                            }
                                
                        } 
                    }
                }
            }              
        }

        private void LookForElseBlock(ref ConditionalBlock block) {
            for (int i = block.GetIndex() + 1; i <= maxIndex; i++) {
                BehaviourBlock currentBlock = GetBlock(i, block.GetIndentation());

                if(currentBlock != null) {
                    if(currentBlock.GetType() == BehaviourBlock.BlockType.CONDITIONAL) {
                        ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

                        if(!block.Equals(conditionalBlock) && conditionalBlock.GetConditionalType() != ConditionalBlock.ConditionalType.IF) {
                            block.SetElseBlock(conditionalBlock);

                            return;   
                        }
                            
                    }
                }
            }
        }

        private void LookForEndOfConditional(ref ConditionalBlock block) {
            // We need to check where the conditional group ends
            int endOfConditional = this.maxIndex;

            for(int i = block.GetIndex() + 1; i < this.maxIndex; i++) {
                // The conditional ends when we encounter a block, which is not the else block, with the same indentation as the conditional block
                BehaviourBlock currentBlock = GetBlock(i);
                if(currentBlock != null) {

                    if(currentBlock.GetType() == BehaviourBlock.BlockType.CONDITIONAL) {
                        ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;
                        if(block.GetElseBlock().Equals(conditionalBlock)) continue;
                    }

                    if(currentBlock.GetIndentation() <= block.GetIndentation()) {
                        endOfConditional = currentBlock.GetIndex();
                        break;
                    }
                }
            }

            block.SetEndOfConditional(endOfConditional);
        }


        public IEnumerator ExecuteBehavior() {
            // Execute the first block
            BehaviourBlock currentBlock = GetBlock();

            Debug.Log("Execute -> " + currentBlock.GetBlockLocation().ToString());

            BlockLocation nextBlockLocation = currentBlock.Execute(); 

            yield return null;

            // Now, we go through each block in the behaviour until we reach the end of it
            while(nextBlockLocation.GetIndex() <= this.maxIndex) {
                yield return null;

                // Get the next block
                currentBlock = GetBlock(nextBlockLocation.GetIndex(), nextBlockLocation.GetIndentation());

                if(currentBlock != null) {   
                    Debug.Log("Execute -> " + currentBlock.GetBlockLocation().ToString());

                    // Execute the block and get the next block location
                    nextBlockLocation = currentBlock.Execute();
                }
            }
        }


        public void SetBlock(int index, int indentation, BehaviourBlock block) {
            this.blocks[index - 1, indentation - 1] = block;
        }

        public BehaviourBlock GetBlock()
        {
            for (int i = 1; i <= maxIndex; i++)
                if (GetBlock(i) != null)
                    return GetBlock(i);
            
            return null;
        }

        public BehaviourBlock GetBlock(int index)
        {
            if(index == -1) return GetBlock();

            if      (GetBlock(index, 1) != null) return GetBlock(index, 1);
            else if (GetBlock(index, 2) != null) return GetBlock(index, 2);
                                                 return GetBlock(index, 3);
        }

        public BehaviourBlock GetBlock(int index, int indentation)
        {
            BehaviourBlock block = indentation == -1 ? GetBlock(index) : blocks[index - 1, indentation - 1];
            return block;
        }

        public int GetMaxIndex() {
            return this.maxIndex;
        }


        public void Print() {
            Debug.Log(this.ToString(0));
        }

        public string ToString(int indentation) {
            string message = "";

            for(int i = 1; i <= maxIndex; i++)
                for(int j = 1; j <= 3; j++)
                    if (GetBlock(i, j) != null){
                        for(int k = 0; k < indentation; k++) message += "\t";
                        message += "Block (" + GetBlock(i, j).GetIndex().ToString() + ", " + GetBlock(i, j).GetIndentation().ToString() + ") = " + GetBlock(i, j).ToString() + "\n";
                    }
                    
            return message;
        }
    }
}

