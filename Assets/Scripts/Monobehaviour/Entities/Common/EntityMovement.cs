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
        private float jumpForce = 400f;
        [SerializeField]
        private float gravity = 9.8f;

        [SerializeField]
        private LayerMask whatIsGround;

        [SerializeField] [Range(0, .3f)]
        private float smoothingFactor = .05f;

        private Vector3 velocity = Vector3.zero;

        private Rigidbody2D rigidBody;
        private CircleCollider2D circleCollider; 

        private bool shouldJump = false;
        private bool isFacingRight = true;


        private void Awake() 
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
            this.circleCollider =  this.GetComponent<CircleCollider2D>();
        }

        private void FixedUpdate() {
            Vector2 targetVelocity = rigidBody.velocity;
            
            // Jump if requested
            targetVelocity.y = IsGrounded() && shouldJump ? jumpForce : 0f;

            float gravityReduction =  gravity * Mathf.Pow(Time.deltaTime, 2);
            if(targetVelocity.y >= gravityReduction) targetVelocity.y -= gravityReduction;

            rigidBody.velocity.Set(targetVelocity.x, targetVelocity.y);
        }


        public void Move(float x, float y) 
        {
            // Compute the new target velocity, smooth it, and apply it
            Vector3 targetVelocity = new Vector2(Mathf.Round(x), 0f) * speed;
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
            Collider2D[] rayCastHit = Physics2D.OverlapCircleAll(circleCollider.bounds.center, circleCollider.radius + .5f, whatIsGround);
            isGrounded = rayCastHit.Length > 0;

            // Debug the raycast performed in overlapcircle
            Color rayColor = isGrounded ? Color.green : Color.red;
            DebugUtils.DebugCircleRayCast(circleCollider.bounds.center, circleCollider.radius, rayColor);

            return isGrounded;
        }
    }
}


