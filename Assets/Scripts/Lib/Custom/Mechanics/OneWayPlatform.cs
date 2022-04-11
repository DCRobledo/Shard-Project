using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Lib.Custom
{
    public class OneWayPlatform : MonoBehaviour
    {
        private float rotationalOffset;

        private PlatformEffector2D effector2D;


        private void Awake() {
            this.effector2D = this.GetComponent<PlatformEffector2D>();

            this.rotationalOffset = effector2D.rotationalOffset;
        }


        public void AllowDrop() {
            effector2D.rotationalOffset = 180f;

            StartCoroutine(ResetRotationalOffset());
        }

        private IEnumerator ResetRotationalOffset() {
            yield return new WaitForSeconds(0.5f);

            effector2D.rotationalOffset = rotationalOffset;
        }
    }
}


