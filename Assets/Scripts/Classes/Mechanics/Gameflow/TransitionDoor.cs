using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class TransitionDoor : MonoBehaviour
    {
        [SerializeField]
        private string sceneToLoad;

        public static Action<string> transitionDoorEvent;

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player")
                transitionDoorEvent?.Invoke(sceneToLoad);
        }
    }
}


