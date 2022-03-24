using Shard.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Shard.Gameflow
{
    public class GameFlowManagement : MonoBehaviour
    {
        [SerializeField]
        private bool playStartSequence = true;

        [SerializeField]
        private GameObject levelTransitionGO;
        private Animator levelTransitionAnimator;

        [SerializeField]
        private GameObject startDoor;
        private Animator startDoorAnimator;

        [SerializeField]
        private GameObject endDoor;
        private Animator endDoorAnimator;

        [SerializeField]
        private GameObject winTrigger;

        private GameObject player;
        private Animator playerAnimator;


        private void Awake() {
            this.player = GameObject.Find("player");

            this.playerAnimator = player.GetComponent<Animator>();
            this.levelTransitionAnimator = levelTransitionGO.GetComponent<Animator>();
            this.startDoorAnimator       = startDoor.GetComponent<Animator>();
            this.endDoorAnimator         = endDoor.GetComponent<Animator>();

            this.levelTransitionGO.SetActive(true);

            if (!playStartSequence)
                levelTransitionAnimator.Play("idle_open");

            winTrigger.GetComponent<TilemapRenderer>().enabled = false;
        }

        private void Start() {
            if (playStartSequence)
                StartCoroutine(StartLevelSequence());
            else
                PlayerController.Instance.EnableInput();
        }

        private void OnEnable() {
            WinTrigger.endLevelEvent += EndLevel;
        }

        private void OnDisable() {
            WinTrigger.endLevelEvent -= EndLevel;
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
        }

        private IEnumerator PlayerFadeIn(Color playerColor) {
            do {
                playerColor.a += 0.08f;

                player.GetComponent<SpriteRenderer>().color = playerColor;

                yield return null;

            } while (playerColor.a < 1f);
        }

        private void EndLevel() {
            // Disable robot functionality

            // Disable player input
            PlayerController.Instance.DisableInput();

            // Start end level sequence
            StartCoroutine(EndLevelSequence());
        }

        private IEnumerator EndLevelSequence() {
            // Play first half of fade out animation

            // Open end door
            endDoorAnimator.SetTrigger("open");
            //yield return new WaitForSeconds(endDoorAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.averageDuration);

            // Walk player into door

            // Fade out player

            // Close end door
            endDoorAnimator.SetTrigger("close");
            //yield return new WaitForSeconds(endDoorAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.averageDuration);

            // Play second half of fade out animation
            levelTransitionAnimator.SetTrigger("endLevel");

            // Load next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            yield return null;
        }

    }
}


