using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class DeathTrigger : MonoBehaviour
    {
        public static Action<string> deathTriggerEvent;

        private void OnTriggerEnter2D(Collider2D other) {
            deathTriggerEvent?.Invoke(other.tag);
        }
    }
}
