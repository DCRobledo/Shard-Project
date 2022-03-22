using Shard.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shard.Gameflow
{
    public class GameFlowManagement : MonoBehaviour
    {
        [SerializeField]
        private bool playStartAnimation = true;

        [SerializeField]
        private GameObject blackPanel;
        private Animator animator;


        private void Awake() {
            this.animator = blackPanel.GetComponent<Animator>();

            if (!playStartAnimation) animator.Play("idle_transparent");
        }

        private void Start() {
            StartCoroutine(StartLevel());
        }

        private void OnEnable() {
            WinTrigger.endLevelEvent += EndLevel;
        }

        private void OnDisable() {
            WinTrigger.endLevelEvent -= EndLevel;
        }


        private IEnumerator StartLevel() {
            // Wait for start animation to finish
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Activate player input
            PlayerController.Instance.EnableInput();

            // Activate robot functionality
        }

        private void EndLevel() {
            // Disable robot functionality

            // Disable player input
            PlayerController.Instance.DisableInput();

            // Start end level sequence
            StartCoroutine(AfterEndLevel());
        }

        private IEnumerator AfterEndLevel() {
            // Play end animation
            animator.SetTrigger("endLevel");

            // Wait for end animation to finish
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Load next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


