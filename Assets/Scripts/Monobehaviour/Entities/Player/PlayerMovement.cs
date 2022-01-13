using Shard.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public class PlayerMovement : EntityMovement
    {
        
        protected void Awake() 
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
            this.circleCollider =  this.GetComponent<CircleCollider2D>();
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
            if(IsGrounded() && shouldJump) {
                shouldJump = false;

                rigidBody.AddForce(new Vector2(0f, jumpForce));
            }
                
        }

        public override void Jump() { shouldJump = true; } 

        public override bool IsGrounded() 
        {
            bool isGrounded;

            // Check ground through raycasting the circle collider
            Collider2D[] rayCastHit = Physics2D.OverlapCircleAll(circleCollider.bounds.center, circleCollider.radius + .5f, whatIsGround);
            isGrounded = rayCastHit.Length > 0;

            // Debug the raycast performed in overlapcircle
            Color rayColor = isGrounded ? Color.green : Color.red;
            new DebugUtils().DebugCircleRayCast(circleCollider.bounds.center, circleCollider.radius, rayColor);

            return isGrounded;
        }
    }
}


