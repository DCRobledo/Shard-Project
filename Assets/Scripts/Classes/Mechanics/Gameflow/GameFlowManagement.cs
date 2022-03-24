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
        private bool playStartAnimation = true;

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


        private void Awake() {
            this.levelTransitionGO.SetActive(true);

            this.levelTransitionAnimator = levelTransitionGO.GetComponent<Animator>();
            this.startDoorAnimator       = startDoor.GetComponent<Animator>();
            this.endDoorAnimator         = endDoor.GetComponent<Animator>();

            if (!playStartAnimation) levelTransitionAnimator.Play("idle_open");

            winTrigger.GetComponent<TilemapRenderer>().enabled = false;
        }

        private void Start() {
            StartCoroutine(StartLevelSequence());
        }

        private void OnEnable() {
            WinTrigger.endLevelEvent += EndLevel;
        }

        private void OnDisable() {
            WinTrigger.endLevelEvent -= EndLevel;
        }


        private IEnumerator StartLevelSequence() {
            // Wait for first half opening to finish
            yield return new WaitForSeconds(levelTransitionAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Open start door
            startDoorAnimator.SetTrigger("open");
            yield return new WaitForSeconds(2);

            // Fade in player

            // Play second half opening
            levelTransitionAnimator.SetTrigger("openSecondHalf");
            yield return new WaitForSeconds(levelTransitionAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Activate player input
            PlayerController.Instance.EnableInput();

            // Activate robot functionality

            // Close door
            startDoorAnimator.SetTrigger("close");
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


