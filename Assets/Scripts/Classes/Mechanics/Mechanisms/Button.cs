using Shard.Lib.Custom;
using System;
using System.Linq;
using UnityEngine;

namespace Shard.Mechanisms
{
    public class Button : MonoBehaviour
    {
        #if UNITY_EDITOR 
            [TagSelector]
         #endif
        [SerializeField]
        protected string[] canBePressedBy = new string[] { };

        public Action buttonPressEvent;


        private void OnTriggerEnter2D(Collider2D other) {
            if(canBePressedBy.Contains(other.tag))
                buttonPressEvent?.Invoke();
        }
    }
}


