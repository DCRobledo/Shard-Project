using Shard.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class CameraTrigger : MonoBehaviour
    {
        [SerializeField]
        private CameraEnum.VirtualCamera originCamera;
        [SerializeField]
        private CameraEnum.VirtualCamera targetCamera;

        private bool isInTargetCamera = false;


        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player") {
                if(isInTargetCamera) CameraSwitcher.SwitchCamera(originCamera);
                else                 CameraSwitcher.SwitchCamera(targetCamera);

                isInTargetCamera = !isInTargetCamera;

                StartCoroutine(CameraTriggerDelay());
            }
        }


        private IEnumerator CameraTriggerDelay() {
            this.GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(0.5f);

            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}


