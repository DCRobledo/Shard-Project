using Shard.Lib.Custom;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Shard.Entities
{
    public abstract class EntityMovement : MonoBehaviour
    {
        [SerializeField]
        protected float speed = 10f;
        [SerializeField]
        protected float jumpForce = 12f;
        [SerializeField] [Range(1, 10)]
        protected float fallMultiplier = 2.5f;
        [SerializeField] [Range(1, 10)]
        protected float lowJumpMultiplier = 2f;
        [SerializeField] [Range(1, 10)]
        protected float crouchFactor = 2f;

        [SerializeField]
        protected LayerMask whatIsGround;

        [SerializeField] [Range(0, .3f)]
        protected float smoothingFactor = .05f;

        protected Vector3 velocity = Vector3.zero;

        protected Rigidbody2D rigidBody;
        protected BoxCollider2D boxCollider2D; 

        protected bool isAscending = false;
        protected bool isFalling = false;
        protected bool shouldCheckGround = true;
        protected bool isFacingRight = true;

        [SerializeField]
        protected UnityEvent jumpTrigger;

        [SerializeField]
        protected UnityEvent landTrigger;


        private void Awake() 
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
            this.boxCollider2D =  this.GetComponent<BoxCollider2D>();

            
        }

        private void FixedUpdate() {
            // Jump if requested
            if (isAscending && IsGrounded()) {
                jumpTrigger?.Invoke();

                rigidBody.velocity += Vector2.up * jumpForce;
            }
                
            // Check for landing
            if (isFalling && IsGrounded()) {
                landTrigger?.Invoke();

                isFalling = false;
            }
                        
            ApplyGravity(this.fallMultiplier, this.lowJumpMultiplier);

            isFacingRight = this.transform.localScale.x >= 0f; 
        }


        public virtual void Move() {}

        public virtual void Jump() {}

        public void Move(float x, float y) {
            // Compute the new target velocity, smooth it, and apply it
            Vector3 targetVelocity = new Vector2(Mathf.Round(x) * speed, rigidBody.velocity.y);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, smoothingFactor);

            // Flip if necessary
            if      (x > 0 && this.gameObject.transform.localScale.x < 0) Flip();
            else if (x < 0 && this.gameObject.transform.localScale.x > 0) Flip();
        }

        public void Jump(bool jump) {
            this.isAscending = jump;
        }


        public void Flip() {
            // Flip the entity
            GameObject entity = this.gameObject;
            TransformUtils.FlipObject(ref entity);
            isFacingRight = !isFacingRight;

            // Flip the anchor point of grab
            this.GetComponent<RelativeJoint2D>().linearOffset = new Vector2(this.GetComponent<RelativeJoint2D>().linearOffset.x * -1, this.GetComponent<RelativeJoint2D>().linearOffset.y);
        
            // Flip the grabbed entity if it exists
            GameObject grabbedEntity = this.GetComponent<RelativeJoint2D>().connectedBody?.gameObject;
            if(grabbedEntity != null) TransformUtils.FlipObject(ref grabbedEntity);
        }

        private void ApplyGravity(float fallMultiplier, float lowJumpMultiplier) {
            // Regular jump gravity
            if(rigidBody.velocity.y < 0) {
                rigidBody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime;

                isAscending = false;
                isFalling = true;
            }

            // Low jump gravity
            else if (rigidBody.velocity.y > 0 && !isAscending)
                rigidBody.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        private bool IsGrounded() 
        {
            return Detection.DetectGround(this.boxCollider2D, this.whatIsGround, .25f, false);
        }

        public void Crouch(bool crouch) {
            // Modify player's y scale
            Vector3 scale = this.transform.localScale;
            scale.y = crouch ? scale.y - crouchFactor * .1f : scale.y + crouchFactor * .1f;
            this.transform.localScale = scale;

            // And movement speed
            speed = crouch ? speed - crouchFactor : speed + crouchFactor;
        }
    
        public Vector2 GetVelocity() {
            return this.rigidBody.velocity;
        }
    }
}


