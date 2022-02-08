using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockSpace : MonoBehaviour
    {
        public int index;

        private bool isInScrollArea;

        private void OnTriggerEnter2D(Collider2D other) {
            isInScrollArea = other.transform.tag == "Behaviour Block" ? isInScrollArea : other.transform.tag == "Scrollable Block Area";
        }

        public bool canBeUsed() { return isInScrollArea; }
    }
}


