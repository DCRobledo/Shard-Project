using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Shard.UI.ProgrammingUI
{
    public class BlockManagement : MonoBehaviour
    {
        [SerializeField] [Range(0, 20)]
        private int maxMemory = 10;

        [SerializeField] [Range(1, 100)]
        private int indentationFactor = 10;

        public GameObject memoryLeft;
        private TextMeshProUGUI memoryLeftText;


        private void Awake() {
            memoryLeftText = memoryLeft.GetComponent<TextMeshProUGUI>();
        }

        private void Update() {
            memoryLeftText.SetText((maxMemory - GetNumOfBlocks()) + " KB FREE");
        }


        public void PlaceBlock(int blockSpace, int indentation, GameObject block) {
            // Check if there is already a block in the space
            if(!IsOutOfMemory() && !IsThereBlock(blockSpace)) {
                block.transform.SetParent(GetBlockContainer(blockSpace - 1).transform);

                block.GetComponent<RectTransform>().anchoredPosition = CalculateBlockPosition(
                    GetBlockContainer(blockSpace).GetComponent<RectTransform>(),
                    block.GetComponent<RectTransform>(),
                    indentation
                );
                
                //UpdateBlockPosition(blockSpace - 1);
            }

            else Destroy(block.gameObject);

            //UpdateIndentations();
        }

        private void UpdateIndentations() {
            // Check all block spaces
            for (int i = 0; i < this.transform.childCount - 1; i++)
            {
                Transform blockSpaceTransform = GetBlockParent(i).transform;
                BlockSpace blockSpace = blockSpaceTransform.gameObject.GetComponent<BlockSpace>();

                // Check if there is a block within each block space 
                if(IsThereBlock(i)) {
                    BehaviourBlock behaviourBlock = GetBlock(i).GetComponent<BehaviourBlock>();

                    // For conditional blocks, we indent the very next block
                    if(behaviourBlock.blockType == BehaviourBlock.BlockType.IF || behaviourBlock.blockType == BehaviourBlock.BlockType.ELSE || behaviourBlock.blockType == BehaviourBlock.BlockType.ELSEIF) {
                        BlockSpace nextBlockSpace = GetBlockParent(i + 1).GetComponent<BlockSpace>();

                        // Only modify indentation if needed
                        if(blockSpace.GetIndentation() == nextBlockSpace.GetIndentation()) {
                            int indentationModifier = blockSpace.GetIndentation() - nextBlockSpace.GetIndentation() + 1;

                            nextBlockSpace.ModifyIndentation(indentationModifier);

                            // Update block if needed
                            if(IsThereBlock(i + 1))
                                UpdateBlockPosition(i + 1);
                        }
                    }
                }
            }
        }

        private void UpdateBlockPosition(int index) {
            GameObject block = GetBlock(index);

            block.GetComponent<RectTransform>().anchoredPosition = CalculateBlockPosition(
                GetBlockContainer(index).GetComponent<RectTransform>(),
                block.GetComponent<RectTransform>(),
                GetBlockParent(index).GetComponent<BlockSpace>().GetIndentation()
            );
        }

        private Vector3 CalculateBlockPosition(RectTransform blockContainer, RectTransform block, float indentation) {
            Vector3 blockPosition = blockContainer.anchoredPosition;

            blockPosition.x -= blockContainer.sizeDelta.x / 2;
            blockPosition.x += block.sizeDelta.x / 2;

            blockPosition.x += (indentation - 1) * indentationFactor;

            return blockPosition;
        }

        public void ClearBlocks() {
            for (int i = 0; i < this.transform.childCount; i++) {
                GameObject block = GetBlock(i);

                if (block != null) Destroy(block);
            }
        }


        private GameObject GetBlock(int index) {
            return IsThereBlock(index) ? GetBlockParent(index).transform.GetChild(0).transform.GetChild(0).gameObject : null;
        }

        private GameObject GetBlockSpace(int index, int indentationLevel) {
            return GetBlockParent(index).transform.GetChild(2).transform.GetChild(indentationLevel).gameObject;
        }

        private GameObject GetBlockContainer(int index) {
            return GetBlockParent(index).transform.GetChild(0).gameObject;
        }

        private GameObject GetBlockParent(int index) {
            return this.transform.GetChild(index).gameObject;
        }

        private bool IsThereBlock(int index) {
            return GetBlockParent(index).transform.GetChild(0).childCount > 0;
        }

        private int GetNumOfBlocks() {
            int numOfBlocks = 0;

            for (int i = 0; i < this.transform.childCount; i++)
                if (IsThereBlock(i)) numOfBlocks++;

            return numOfBlocks;
        }

        private bool IsOutOfMemory() {
            return GetNumOfBlocks() >= maxMemory;
        }


        private void PrintBlocks() {
            string message = "";

            for(int i = 0; i < this.transform.childCount; i++)
            {
                string blockType = GetBlockParent(i) == null ? "NULL" : GetBlockParent(i).GetComponent<BehaviourBlock>().blockType.ToString();

                message += i + 1 + " -> " + blockType + "\n";
            }

            Debug.Log(message);
        }
    }

}