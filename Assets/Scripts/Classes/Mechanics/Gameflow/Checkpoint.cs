using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class Checkpoint : MonoBehaviour
    {
        public Action<string, Vector3> checkpointItEvent;

        private void OnTriggerEnter2D(Collider2D other) {
            checkpointItEvent?.Invoke(other.transform.tag, other.transform.position);
        }
    }
}


