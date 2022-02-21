using Shard.Lib.Custom;
using System.Collections;
using System;
using UnityEngine;

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

        protected bool canJump = true;
        protected bool shouldJump = false;
        protected bool isFacingRight = true;


        private void Awake() 
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
            this.boxCollider2D =  this.GetComponent<BoxCollider2D>();

            isFacingRight = this.transform.localScale.x >= 0f;
        }

        private void FixedUpdate() {
            // Jump if requested
            if (IsGrounded() && shouldJump)
                rigidBody.velocity += Vector2.up * jumpForce;

            ApplyGravity(this.fallMultiplier, this.lowJumpMultiplier);
        }


        public virtual void Move() {}

        public virtual void Move(float x, float y) {}

        public virtual void Jump() {}

        public virtual void Jump(bool jump) {}


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
                shouldJump = false;
            }
            // Low jump gravity
            else if (rigidBody.velocity.y > 0 && !shouldJump)
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
    
    }
}


