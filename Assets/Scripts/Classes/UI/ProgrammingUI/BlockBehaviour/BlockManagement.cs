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
                block.transform.position = this.transform.GetChild(blockSpace - 1).GetChild(0).transform.position;

                blocks[blockSpace - 1] = block;
            }

            else Destroy(block.gameObject);
        }

        public void RemoveBlock(int blockSpace) {
            blocks[blockSpace - 1] = null;
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