using Shard.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class RobotAnimations : MonoBehaviour
    {
        private Animator animator;


        private void Awake() {
            this.animator = this.GetComponent<Animator>();
        }

        private void Update() {
            Vector2 velocity = GetComponent<RobotMovement>().GetVelocity();

            bool isMoving = velocity.x > 0.01f || velocity.x < -0.01f;

            animator.SetBool("isMoving", isMoving);
        }


        public void SetIsJumping() {
            animator.SetBool("isJumping", true);
        }

        public void UnsetIsJumping() {
            animator.SetBool("isJumping", false);
        }
    }
}
