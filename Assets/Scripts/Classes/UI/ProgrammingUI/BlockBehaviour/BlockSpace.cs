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


        private void Awake() {
            //ResetIndentation();
        }


        private void OnTriggerEnter2D(Collider2D other) {
            isInScrollArea = other.transform.tag == "Behaviour Block" ? isInScrollArea : other.transform.tag == "Scrollable Block Area";
        }

        public bool CanBeUsed() { return isInScrollArea; }

        public void ResetIndentation()
        {
            this.indentation = 1;
        }
        
        public void ModifyIndentation(int modifier)
        {
            this.indentation += modifier;
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


