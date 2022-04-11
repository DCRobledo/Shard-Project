using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockSpace : MonoBehaviour
    {
        [SerializeField]
        private int index;
        [SerializeField]
        private int indentation;

        public bool isInScrollArea;


        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Scrollable Block Area")
                isInScrollArea = true;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(other.tag == "Scrollable Block Area")
                isInScrollArea = false;
        }


        public bool CanBeUsed() {
            return isInScrollArea; 
        }

        public int GetIndex()
        {
            return this.index;
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public int GetIndentation()
        {
            return this.indentation;
        }

        public void SetIndentation(int indentation)
        {
            this.indentation = indentation;
        }
    }
}


