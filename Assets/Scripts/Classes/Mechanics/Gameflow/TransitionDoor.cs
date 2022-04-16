using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class TransitionDoor : MonoBehaviour
    {
        [SerializeField]
        private string sceneToLoad;

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player") {
                this.GetComponent<BoxCollider2D>().enabled = false;

                GameFlowManagement.endLevelEvent?.Invoke(sceneToLoad, this.gameObject);
            }
        }
    }
}


