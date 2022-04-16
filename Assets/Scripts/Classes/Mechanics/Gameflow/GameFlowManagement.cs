using Shard.Controllers;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Cinemachine;

namespace Shard.Gameflow
{
    public class GameFlowManagement : MonoBehaviour
    {
        [SerializeField]
        private bool playStartSequence = true;
        [SerializeField]
        private bool playEndSequence = true;

        [SerializeField]
        private GameObject levelTransitionGO;
        private Animator levelTransitionAnimator;

        [SerializeField]
        private GameObject startDoor;
        private Animator startDoorAnimator;

        private GameObject endDoor;
        private Animator endDoorAnimator;

        private GameObject player;
        private Animator playerAnimator;

        private GameObject mainCamera;
        private CinemachineBrain cameraBrain;

        public static Action<string, GameObject> endLevelEvent;


        private void Awake() {
            this.player = GameObject.Find("player");

            this.mainCamera = GameObject.Find("camera");
            this.cameraBrain = mainCamera.GetComponent<CinemachineBrain>();

            this.playerAnimator          = player.GetComponent<Animator>();
            this.levelTransitionAnimator = levelTransitionGO.GetComponent<Animator>();
            this.startDoorAnimator       = startDoor.GetComponent<Animator>();

            this.levelTransitionGO.SetActive(playStartSequence);
        }

        private void Start() {
            if (playStartSequence)
                StartCoroutine(StartLevelSequence());
            else
                PlayerController.Instance.EnableInput();
        }

        private void OnEnable() {
            endLevelEvent += EndLevel;
        }

        private void OnDisable() {
            endLevelEvent -= EndLevel;
        }


        private IEnumerator StartLevelSequence() {
            // Move player to start door and hide it
            player.transform.localPosition = startDoor.transform.localPosition;

            Color playerColor = Color.white; playerColor.a = 0f;
            player.GetComponent<SpriteRenderer>().color = playerColor;

            playerAnimator.SetBool("isMoving", true);

            // Wait for first half opening to finish
            yield return new WaitForSeconds(levelTransitionAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Open start door
            startDoorAnimator.SetTrigger("open");
            yield return new WaitForSeconds(1.5f);

            // Fade in player
            StartCoroutine(PlayerFadeIn(playerColor));
            yield return new WaitForSeconds(0.5f);

            // Stop player
            playerAnimator.SetBool("isMoving", false);

            // Play second half opening
            yield return new WaitForSeconds(0.25f);
            levelTransitionAnimator.SetTrigger("openSecondHalf");


            // Activate player input
            PlayerController.Instance.EnableInput();

            // Close door
            yield return new WaitForSeconds(0.5f);
            startDoorAnimator.SetTrigger("close");

            yield return new WaitForSeconds(levelTransitionAnimator.GetCurrentAnimatorStateInfo(0).length);
            this.levelTransitionGO.SetActive(false);
        }

        public void EndLevel(string sceneToLoad, GameObject transitionDoor) {
            // Disable player input
            PlayerController.Instance.DisableInput();

            // Start end level sequence
            if(playEndSequence)
                StartCoroutine(EndLevelSequence(sceneToLoad, transitionDoor));
            else {
                if(sceneToLoad.ToLower() == "exit") Application.Quit();
                else SceneManager.LoadScene(sceneToLoad);
            }
                
        }

        private IEnumerator EndLevelSequence(string sceneToLoad, GameObject transitionDoor) {
            this.endDoor = transitionDoor;
            this.endDoorAnimator = transitionDoor.GetComponent<Animator>();

            this.levelTransitionGO.SetActive(true);
            this.levelTransitionAnimator.Play("idle_open");

            // Set camera x_offset to 0
            CinemachineStateDrivenCamera vcam = cameraBrain.ActiveVirtualCamera as CinemachineStateDrivenCamera;
            vcam.LiveChild.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0f;

            // Move player to end door
            StartCoroutine(MovePlayerToDoor(endDoor.transform.localPosition));

            // Play first half of close animation
            levelTransitionAnimator.SetTrigger("closeFirstHalf");
            yield return new WaitForSeconds(levelTransitionAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Open door
            endDoorAnimator.SetTrigger("open");
            yield return new WaitForSeconds(1.5f);

            // Fade out player
            playerAnimator.SetBool("isMoving", true);
            Color playerColor = player.GetComponent<SpriteRenderer>().color;
            yield return PlayerFadeOut(playerColor);

            // Close door
            endDoorAnimator.SetTrigger("close");
            yield return new WaitForSeconds(1.5f);

            // Play second half of close animation
            levelTransitionAnimator.SetTrigger("closeSecondHalf");
            yield return new WaitForSeconds(levelTransitionAnimator.GetCurrentAnimatorStateInfo(0).length + 1f);

            // Load next level or quit
            if(sceneToLoad.ToLower() == "exit") Application.Quit();
            else SceneManager.LoadScene(sceneToLoad);
        }


        private IEnumerator MovePlayerToDoor(Vector3 doorPosition) {
            do {
                playerAnimator.SetBool("isMoving", true);

                float distanceSign = player.transform.localPosition.x < doorPosition.x ? 1f : -1f;

                Vector3 playerPosition = player.transform.localPosition; 

                playerPosition.x += 0.025f * distanceSign;

                player.transform.localPosition = playerPosition;

                yield return null;
            } while (player.transform.localPosition.x < doorPosition.x - 0.1f || player.transform.localPosition.x > doorPosition.x + 0.1f);

             playerAnimator.SetBool("isMoving", false);
        }

        private IEnumerator PlayerFadeIn(Color playerColor) {
            do {
                playerColor.a += 0.08f;

                player.GetComponent<SpriteRenderer>().color = playerColor;

                yield return null;

            } while (playerColor.a < 1f);
        }

        private IEnumerator PlayerFadeOut(Color playerColor) {
            do {
                playerColor.a -= 0.04f;

                player.GetComponent<SpriteRenderer>().color = playerColor;

                yield return null;

            } while (playerColor.a > 0f);
        }
    }
}


