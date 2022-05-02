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

        private Coroutine cameraCoolDown;


        private void Awake() {
            cameraAnimator = stateDrivenCamera.GetComponent<Animator>();
        }

        private void OnEnable() {
            DeathTrigger.deathTriggerEvent += PreventCameraFollow;
        }

        private void OnDisable() {
            DeathTrigger.deathTriggerEvent -= PreventCameraFollow;
        }


        public static void SwitchCamera(CameraEnum.VirtualCamera targetCamera) {
            cameraAnimator.Play(targetCamera.ToString().ToLower());
        }

        public void PreventCameraFollow(string entity) {
            if(entity == "Player" && cameraCoolDown == null) {
                GameObject activeCameraGO = stateDrivenCamera.GetComponent<CinemachineStateDrivenCamera>().LiveChild.VirtualCameraGameObject;
                CinemachineVirtualCamera activeCamera = activeCameraGO.GetComponent<CinemachineVirtualCamera>();
                Transform follow = activeCamera.Follow;

                cameraCoolDown = StartCoroutine(CameraFollowCoolDown(activeCamera, follow));
            }
        }

        private IEnumerator CameraFollowCoolDown(CinemachineVirtualCamera camera, Transform follow) {
            camera.Follow = null;

            yield return new WaitForSeconds(1f);

            camera.Follow = follow;
        }
    }    
}


