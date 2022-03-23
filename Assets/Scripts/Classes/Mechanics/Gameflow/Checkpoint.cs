using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class Checkpoint : MonoBehaviour
    {
        public static Action<string, Vector3> checkpointEvent;

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player" || other.tag == "Robot")
                checkpointEvent?.Invoke(other.tag, other.transform.position);
        }
    }
}


