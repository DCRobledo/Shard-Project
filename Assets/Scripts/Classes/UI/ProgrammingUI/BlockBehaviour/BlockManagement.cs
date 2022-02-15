using System;
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

        [SerializeField]
        private GameObject memoryLeft;
        private TextMeshProUGUI memoryLeftText;

        public static event Action<int, int, List<GameObject>> generateBlockBehaviourEvent;


        private void Awake() {
            memoryLeftText = memoryLeft.GetComponent<TextMeshProUGUI>();
        }

        private void Update() {
            memoryLeftText.SetText((maxMemory - GetNumOfBlocks()) + " KB FREE");
        }


        public void PlaceBlock(int index, int indentation, GameObject block) {
            // Check if there is already a block in the space
            if(!IsOutOfMemory() && !IsThereBlock(index - 1)) {
                
                // Avoid conditional block placing in indentation level 3
                if(block.GetComponent<BehaviourBlock>().GetType() == BehaviourBlock.BlockType.CONDITIONAL && indentation == 3) 
                    Destroy(block.gameObject);
                
                else {
                    block.transform.SetParent(GetBlockContainer(index - 1).transform);

                    block.GetComponent<RectTransform>().anchoredPosition = CalculateBlockPosition(
                        GetBlockContainer(index - 1).GetComponent<RectTransform>(),
                        block.GetComponent<RectTransform>(),
                        indentation
                    );

                    block.GetComponent<BehaviourBlock>().SetBlockLocation(index, indentation);
                }
            }

            else Destroy(block.gameObject);
        }

        private Vector3 CalculateBlockPosition(RectTransform blockContainer, RectTransform block, float indentation) {
            Vector3 blockPosition = blockContainer.anchoredPosition;

            blockPosition.x -= blockContainer.sizeDelta.x / 2;
            blockPosition.x += block.sizeDelta.x / 2;

            blockPosition.x += (indentation - 1) * indentationFactor;

            return blockPosition;
        }

        public void ClearBlocks() {
            for (int i = 1; i < this.transform.childCount + 1; i++) {
                GameObject block = GetBlock(i - 1);

                if (block != null) Destroy(block);
            }
        }

        public void GenerateBlockBehaviour() {
            List<GameObject> blocks = new List<GameObject>();
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < this.transform.childCount + 1; i++) {
                if (IsThereBlock(i - 1)) {
                    GameObject block = GetBlock(i - 1);
                    blocks.Add(block);

                    int blockIndex = block.GetComponent<BehaviourBlock>().GetIndex();
                    if(minIndex != 0) minIndex = blockIndex;
                    if(maxIndex < blockIndex) maxIndex = blockIndex;
                } 
            }

            generateBlockBehaviourEvent?.Invoke(minIndex, maxIndex, blocks);
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

            for (int i = 1; i < this.transform.childCount + 1; i++)
                if (IsThereBlock(i - 1)) numOfBlocks++;

            return numOfBlocks;
        }

        private bool IsOutOfMemory() {
            return GetNumOfBlocks() >= maxMemory;
        }
    }

}