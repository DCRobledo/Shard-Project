using Shard.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator animator;


        private void Awake() {
            this.animator = this.GetComponent<Animator>();
        }

        private void OnEnable() {
            PlayerController.stopTrigger += SetStopTrigger;
            PlayerController.moveTrigger += SetMoveTrigger;
        }

        private void OnDisable() {
            PlayerController.stopTrigger -= SetStopTrigger;
            PlayerController.moveTrigger -= SetMoveTrigger;
        }


        private void SetStopTrigger() {
            animator.SetTrigger("stopTrigger");
        }

        private void SetMoveTrigger() {
            animator.SetTrigger("moveTrigger");
        }
    }
}
