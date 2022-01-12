using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public class PlayerMovement : EntityMovement
    {
        
        private void Awake() {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            
        }

        public override void Move(float x, float y) 
        {
            // Compute the new target velocity, smooth it, and apply it
            Vector3 targetVelocity = new Vector2(x, y) * speed;
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, smoothingFactor);

            // Flip if necessary
            if      (x > 0 && !isFacingRight) isFacingRight = !isFacingRight;
            else if (x < 0 && isFacingRight)  isFacingRight = !isFacingRight;

            // Jump if requested
            if(shouldJump) {
                isGrounded = false;

                rigidBody.AddForce(new Vector2(0f, jumpForce));
            }
        }

        public override void Jump() 
        {
            shouldJump = true;
        } 
    }
}


