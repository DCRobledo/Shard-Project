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
            PlayerController.stopTrigger += UnsetIsMoving;
            PlayerController.moveTrigger += SetIsMoving;
        }

        private void OnDisable() {
            PlayerController.stopTrigger -= UnsetIsMoving;
            PlayerController.moveTrigger -= SetIsMoving;
        }


        public void SetIsMoving() {
            animator.SetBool("isMoving", true);
        }

        public void UnsetIsMoving() {
            animator.SetBool("isMoving", false);
        }

        public void SetIsJumping() {
            animator.SetBool("isJumping", true);
        }

        public void UnsetIsJumping() {
            animator.SetBool("isJumping", false);
        }

        public void SetGrabTrigger() {
            animator.SetTrigger("grabTrigger");
        }
    }
}
