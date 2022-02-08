using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockManagement : MonoBehaviour
    {
        private GameObject[] blocks;

        private void Awake() {
            blocks = new GameObject[this.transform.childCount];
        }

        private void Update() {
            //PrintBlocks();
        }


        public void PlaceBlock(int blockSpace, GameObject block) {
            // Check if there is already a block in the space
            GameObject existingBlock = blocks[blockSpace - 1];

            if(existingBlock == null) {
                block.transform.SetParent(this.transform.GetChild(blockSpace - 1).GetChild(0).transform);
                block.GetComponent<RectTransform>().anchoredPosition = CalculateBlockPosition(
                    this.transform.GetChild(blockSpace - 1).GetChild(0).GetComponent<RectTransform>(),
                    block.GetComponent<RectTransform>(),
                    this.transform.GetChild(blockSpace - 1).GetComponent<BlockSpace>().GetIndentation()
                );

                blocks[blockSpace - 1] = block;
            }

            else Destroy(block.gameObject);

            UpdateIndentations();
        }

        public void RemoveBlock(int blockSpace) {
            blocks[blockSpace - 1] = null;
        }

        private void UpdateIndentations() {
            // Check all block spaces
            for (int i = 0; i < blocks.Length - 1; i++)
            {
                Transform blockSpaceTransform = this.transform.GetChild(i);
                BlockSpace blockSpace = blockSpaceTransform.gameObject.GetComponent<BlockSpace>();
                //blockSpace.gameObject.GetComponent<BlockSpace>().ResetIndentation();

                // Check if there is a block within each block space 
                if(blockSpaceTransform.GetChild(0).childCount > 0) {
                    BehaviourBlock behaviourBlock = blockSpaceTransform.GetChild(0).transform.GetChild(0).GetComponent<BehaviourBlock>();

                    // For conditional blocks, we indent the very next block
                    if(behaviourBlock.blockType == BehaviourBlock.BlockType.IF || behaviourBlock.blockType == BehaviourBlock.BlockType.ELSE || behaviourBlock.blockType == BehaviourBlock.BlockType.ELSEIF) {
                        // Check if there is a block next
                        if(this.transform.GetChild(i + 1).GetChild(0).childCount > 0) {
                            BlockSpace nextBlockSpace = this.transform.GetChild(i + 1).gameObject.GetComponent<BlockSpace>();

                            // Only modify indentation if needed
                            if(blockSpace.GetIndentation() == nextBlockSpace.GetIndentation()) {
                                nextBlockSpace.ModifyIndentation(1);

                                UpdateBlockPosition(i + 1);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateBlockPosition(int index) {
            GameObject blockSpace = this.transform.GetChild(index).gameObject;
            GameObject blockContainer = blockSpace.transform.GetChild(0).gameObject;
            GameObject block = blockContainer.transform.GetChild(0).gameObject;

            block.GetComponent<RectTransform>().anchoredPosition = CalculateBlockPosition(
                blockContainer.GetComponent<RectTransform>(),
                block.GetComponent<RectTransform>(),
                blockSpace.GetComponent<BlockSpace>().GetIndentation()
            );
        }

        private Vector3 CalculateBlockPosition(RectTransform blockContainer, RectTransform block, float indentation) {
            Vector3 blockPosition = blockContainer.anchoredPosition;

            blockPosition.x -= blockContainer.sizeDelta.x / 2;
            blockPosition.x += block.sizeDelta.x / 2;

            blockPosition.x += indentation;

            return blockPosition;
        }


        private void PrintBlocks() {
            string message = "";

            for(int i = 0; i < blocks.Length; i++)
            {
                string blockType = blocks[i] == null ? "NULL" : blocks[i].GetComponent<BehaviourBlock>().blockType.ToString();

                message += i + 1 + " -> " + blockType + "\n";
            }

            Debug.Log(message);
        }
    }

}