using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void OnEnable() {
            WinTrigger.endLevelEvent += EndLevel;
        }

        private void OnDisable() {
            WinTrigger.endLevelEvent -= EndLevel;
        }


        private void EndLevel() {
            animator.SetTrigger("endLevel");
        }
    }
}


