using Shard.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f;
        [SerializeField]
        private float jumpForce = 12f;
        [SerializeField] [Range(1, 10)]
        private float fallMultiplier = 2.5f;

        [SerializeField]
        private LayerMask whatIsGround;

        [SerializeField] [Range(0, .3f)]
        private float smoothingFactor = .05f;

        private Vector3 velocity = Vector3.zero;

        private Rigidbody2D rigidBody;
        private BoxCollider2D boxCollider2D; 

        private bool shouldJump = false;
        private bool isFacingRight = true;


        private void Awake() 
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
            this.boxCollider2D =  this.GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate() {
            // Jump if requested
            if (IsGrounded() && shouldJump) {
                shouldJump = false;
                //rigidBody.AddForce(new Vector2(0f, jumpForce));

                rigidBody.velocity += Vector2.up * jumpForce;
                if(rigidBody.velocity.y < 0)
                    rigidBody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime;
		    }
        }


        public void Move(float x, float y) 
        {
            // Compute the new target velocity, smooth it, and apply it
            Vector3 targetVelocity = new Vector2(Mathf.Round(x) * speed, rigidBody.velocity.y);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, smoothingFactor);

            // Flip if necessary
            if      (x > 0 && !isFacingRight) isFacingRight = !isFacingRight;
            else if (x < 0 && isFacingRight)  isFacingRight = !isFacingRight;
        }


        public void Jump() { shouldJump = true; } 

        public bool IsGrounded() 
        {
            bool isGrounded;

            // Check ground through raycasting the circle collider
            RaycastHit2D rayCastHit = 
            Physics2D.BoxCast(
                boxCollider2D.bounds.center,
                boxCollider2D.bounds.size,
                0f,
                Vector2.down,
                1f,
                whatIsGround
            );      

            isGrounded = rayCastHit.collider != null;

            // Debug the raycast performed in overlapcircle
            Color rayColor = isGrounded ? Color.green : Color.red;
            DebugUtils.DebugBoxRayCast(
                boxCollider2D.bounds.center,
                boxCollider2D.bounds.extents.x,
                boxCollider2D.bounds.extents.y + .5f,
                rayColor
            );

            return isGrounded;
        }
    }
}


