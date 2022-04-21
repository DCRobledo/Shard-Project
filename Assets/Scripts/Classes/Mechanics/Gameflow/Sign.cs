using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class Sign : MonoBehaviour
    {
        private void Awake() {
            // Activate overlay
            this.transform.GetChild(0).gameObject.SetActive(true);

            // Disable light
            this.transform.GetChild(1).gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player") {
                // Prevent multiple triggers
                this.GetComponent<BoxCollider2D>().enabled = false;

                // Disable overlay
                this.transform.GetChild(0).gameObject.SetActive(false);

                // Activate light
                this.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}


