using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class WinTrigger : MonoBehaviour
    {
        public static Action endLevelEvent;

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player") endLevelEvent?.Invoke();
        }
    }
}
