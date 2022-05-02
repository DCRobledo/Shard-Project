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
            // Play the camera state with the same name as the target camera
            cameraAnimator.Play(targetCamera.ToString().ToLower());
        }

        public void PreventCameraFollow(string entity) {
            // We don't want the camera following the player when it falls
            if(entity == "Player" && cameraCoolDown == null) {
                // Get the active virtual camera
                GameObject activeCameraGO = stateDrivenCamera.GetComponent<CinemachineStateDrivenCamera>().LiveChild.VirtualCameraGameObject;
                CinemachineVirtualCamera activeCamera = activeCameraGO.GetComponent<CinemachineVirtualCamera>();

                // Get the current follow target
                Transform follow = activeCamera.Follow;

                // And remove it for a little period of time
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


