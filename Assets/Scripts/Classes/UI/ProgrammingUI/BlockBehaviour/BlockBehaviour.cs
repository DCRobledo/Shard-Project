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

            AssingElseBlocks();
            CreateSubBehaviours();
        }

        private void AssingElseBlocks() {
            // Look for all IF or ELSE_IF blocks
            for (int i = 1; i <= maxIndex; i++) {
                for(int j = 1; j <= 3; j++) {

                    BehaviourBlock currentBlock = GetBlock(i, j);
                    if(currentBlock != null) {

                        if(currentBlock.GetType() == BehaviourBlock.BlockType.CONDITIONAL) {
                            ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

                            // Link them with an ELSE block, if it exists
                            if(conditionalBlock.GetConditionalType() != ConditionalBlock.ConditionalType.ELSE)
                                LookForElseBlock(ref conditionalBlock);
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
                        
                        // This prevents two blocks to have the same elseBlock
                        //if (conditionalBlock.GetConditionalType() != ConditionalBlock.ConditionalType.IF) return;

                        if(!block.Equals(conditionalBlock))
                            block.SetElseBlock(conditionalBlock); return;   
                    }
                }
            }
        }

        private void CreateSubBehaviours() {
            // Each conditional block creates sub-behaviours for both of its branches
            for(int i = 1; i <= maxIndex; i++) {
                for(int j = 1; j <= 3; j++) {
                    BehaviourBlock currentBlock = GetBlock(i, j);
                    if(currentBlock != null) {

                        if(currentBlock.GetType() == BehaviourBlock.BlockType.CONDITIONAL) {
                            ConditionalBlock conditionalBlock = currentBlock as ConditionalBlock;

                            if(conditionalBlock.GetConditionalType() != ConditionalBlock.ConditionalType.ELSE) {
                                // We always have a true sub-behaviour
                                conditionalBlock.SetSubBehaviour(true, CreateSubBehaviour(conditionalBlock.GetIndex(), conditionalBlock));

                                // And only a false sub-behaviour if there is an else block
                                if(conditionalBlock.GetElseBlock() != null && conditionalBlock.GetElseBlock().GetConditionalType() == ConditionalBlock.ConditionalType.ELSE)
                                    conditionalBlock.SetSubBehaviour(false, CreateSubBehaviour(conditionalBlock.GetElseBlock().GetIndex(), conditionalBlock));
                            }
                        }
                    }
                }
            } 
        }

        private BlockBehaviour CreateSubBehaviour(int startIndex, ConditionalBlock block) {
            List<GameObject> blocks = new List<GameObject>();
            int maxIndex = 0;

            // We add all blocks until que find one with the same indentation as the conditional block
            for(int i = startIndex + 1; i <= this.maxIndex; i++) {
                BehaviourBlock currentBlock = GetBlock(i);
              
                if (currentBlock != null) {

                    if(currentBlock.GetIndentation() == block.GetIndentation())
                        break;

                    blocks.Add(currentBlock.gameObject);

                    // If a block is part of a sub-behaviour, it is no longer part of the main behaviour
                    if (GetBlock(currentBlock.GetIndex(), currentBlock.GetIndentation()) != null)
                        SetBlock(currentBlock.GetIndex(), currentBlock.GetIndentation(), null);

                    if(maxIndex < currentBlock.GetIndex()) maxIndex = currentBlock.GetIndex();
                }
            }

            return new BlockBehaviour(maxIndex, blocks);
        }


        public void ExecuteBehavior() {
            // Execute the first block
            BehaviourBlock currentBlock = GetBlock();

            BlockLocation nextBlockLocation = currentBlock.Execute(); 

            // Now, we go through each block in the behaviour until we reach the end of it
            while(nextBlockLocation.GetIndex() < this.maxIndex) {
                if(currentBlock != null) {
                    Debug.Log("Execute -> " + currentBlock.GetBlockLocation().ToString());

                    // Execute the block and get the next block location
                    nextBlockLocation = currentBlock.Execute();   

                    // Get the next block
                    currentBlock = GetBlock(nextBlockLocation.GetIndex(), nextBlockLocation.GetIndentation());
                }
            }
        }


        private void SetBlock(int index, int indentation, BehaviourBlock block) {
            this.blocks[index - 1, indentation - 1] = block;
        }

        private BehaviourBlock GetBlock()
        {
            for (int i = 1; i <= maxIndex; i++)
                if (GetBlock(i) != null)
                    return GetBlock(i);
            
            return null;
        }

        private BehaviourBlock GetBlock(int index)
        {
            if(index == -1) return GetBlock();

            if      (GetBlock(index, 1) != null) return GetBlock(index, 1);
            else if (GetBlock(index, 2) != null) return GetBlock(index, 2);
                                                 return GetBlock(index, 3);
        }

        private BehaviourBlock GetBlock(int index, int indentation)
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

