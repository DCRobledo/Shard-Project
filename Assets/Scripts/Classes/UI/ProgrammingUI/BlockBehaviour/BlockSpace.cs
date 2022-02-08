using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockSpace : MonoBehaviour
    {
        public int index;
        
        public int indentation = 1;

        private bool isInScrollArea;


        private void Awake() {
            ResetIndentation();
        }


        private void OnTriggerEnter2D(Collider2D other) {
            isInScrollArea = other.transform.tag == "Behaviour Block" ? isInScrollArea : other.transform.tag == "Scrollable Block Area";
        }

        public bool CanBeUsed() { return isInScrollArea; }

        public int GetIndentation()
        {
            return this.indentation;
        }

        public void ResetIndentation()
        {
            this.indentation = 1;
        }
        
        public void ModifyIndentation(int modifier)
        {
            this.indentation += modifier;
        }
    }
}


