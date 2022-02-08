using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BlockSpace : MonoBehaviour
    {
        public int index;
        
        [SerializeField]
        private int indentationFactor = 5;
        private float indentation;

        private bool isInScrollArea;


        private void Awake() {
            ResetIndentation();
        }


        private void OnTriggerEnter2D(Collider2D other) {
            isInScrollArea = other.transform.tag == "Behaviour Block" ? isInScrollArea : other.transform.tag == "Scrollable Block Area";
        }

        public bool canBeUsed() { return isInScrollArea; }

        public float GetIndentation()
        {
            return this.indentation;
        }

        public void ResetIndentation()
        {
            this.indentation += indentationFactor;
        }
        public void ModifyIndentation(float modifier)
        {
            this.indentation += indentationFactor * modifier * 5;
        }
    }
}


