using Shard.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Shard.Gameflow
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainCamera;
        [SerializeField]
        private GameObject stateDrivenCamera;
    
        private static Animator cameraAnimator;


        private void Awake() {
            cameraAnimator = stateDrivenCamera.GetComponent<Animator>();
        }


        public static void SwitchCamera(CameraEnum.VirtualCamera targetCamera) {
            cameraAnimator.Play(targetCamera.ToString().ToLower());
        }
    }    
}


