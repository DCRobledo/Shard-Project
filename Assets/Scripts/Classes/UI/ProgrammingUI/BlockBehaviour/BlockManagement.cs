using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockManagement : MonoBehaviour
    {
        private List<(int, BehaviourBlock)> blocks = new List<(int, BehaviourBlock)>();

        private int numOfBlocks = 7;

        private void Update() {
            UpdateBlocks();
        }


        private void UpdateBlocks() {
            for (int i = 0; i < numOfBlocks; i++)
            {
                GameObject spaceBlock = this.transform.GetChild(i).gameObject;

                if(spaceBlock.transform.childCount > 0)
                    blocks.Add((i + 1, spaceBlock.transform.GetChild(0).gameObject.GetComponent<BehaviourBlock>()));
            }
        }

        private void PrintBlocks() {
            string message = "";

            foreach ((int, BehaviourBlock) block in blocks)
            {
                message += block.Item1 + " -> " + block.Item2.blockType + "\n";
            }

            Debug.Log(message);
        }
    }

}