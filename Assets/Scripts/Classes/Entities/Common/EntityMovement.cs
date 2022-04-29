using Shard.Lib.Custom;
using Shard.Controllers;
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
        protected PolygonCollider2D polygonCollider2D; 
        
        protected bool isJumpOnCooldown = false;
        protected bool isAscending = false;
        protected bool shouldCheckForLand = true;
        protected bool forceJump = false;
        protected bool isPressingDown = false;
        protected bool isFacingRight = true;
        protected bool isCrouched = false;

        [SerializeField]
        protected UnityEvent jumpTrigger;

        [SerializeField]
        protected UnityEvent landTrigger;


        private void Awake() 
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
            this.polygonCollider2D =  this.GetComponent<PolygonCollider2D>();
        }

        protected void FixedUpdate() {
            // Check for landing
            if (shouldCheckForLand && !isAscending && IsGrounded()) {
                landTrigger?.Invoke();

                shouldCheckForLand = false;
            }
                        
            ApplyGravity(this.fallMultiplier, this.lowJumpMultiplier);

            isFacingRight = this.transform.localScale.x >= 0f; 
        }


        public virtual void Move() {}

        public virtual void Jump() {}

        public virtual void Drop() {
            // Check if we are in a platform
            RaycastHit2D rayCastHit = 
            Physics2D.Raycast (
                polygonCollider2D.bounds.center,
                Vector2.down,
                polygonCollider2D.bounds.size.x + 0.5f,
                LayerMask.GetMask("Platforms")
            );

            if(rayCastHit.collider != null) {
                // If we are, allow drop on it
                GameObject platform = rayCastHit.collider.gameObject;
                platform.GetComponent<OneWayPlatform>().AllowDrop();
            }
        }

        public void Move(float x, float y) {
            // Compute the new target velocity, smooth it, and apply it
            Vector3 targetVelocity = new Vector2(Mathf.Round(x) * speed, rigidBody.velocity.y);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, smoothingFactor);

            // Flip if necessary
            if      (x > 0 && this.gameObject.transform.localScale.x < 0) Flip();
            else if (x < 0 && this.gameObject.transform.localScale.x > 0) Flip();
        }

        public void Jump(bool jump) {
            // Releasing the jump is always allowed
            if(!jump) this.isAscending = jump;

            // Check if we want to drop or jump
            else if(this.isPressingDown) Drop();

            // But we can only jump if we are grounded
            else if(forceJump || (IsGrounded() && !isJumpOnCooldown)) {
                jumpTrigger?.Invoke();

                rigidBody.velocity += Vector2.up * jumpForce;

                StartCoroutine(JumpCoolDown());

                if(forceJump) 
                    forceJump = false;
            }
        }

        private IEnumerator JumpCoolDown() {
            isJumpOnCooldown = true;

            yield return new WaitForSeconds(0.2f);

            isJumpOnCooldown = false;
        }


        public virtual void Flip() {
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

                shouldCheckForLand = true;
            }

            // Low jump gravity
            else if (rigidBody.velocity.y > 0 && !isAscending)
                rigidBody.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        private bool IsGrounded() 
        {
            return Detection.DetectGround(this.polygonCollider2D, this.whatIsGround, .25f, false);
        }

        public virtual void Crouch(bool crouch) {
            // Modify y scale
            Vector3 scale = this.transform.localScale;
            scale.y = crouch ? scale.y - crouchFactor * .1f : scale.y + crouchFactor * .1f;
            this.transform.localScale = scale;

            // And movement speed
            speed = crouch ? speed - crouchFactor : speed + crouchFactor;

            this.isCrouched = crouch;
        }
    
        public Vector2 GetVelocity() {
            return this.rigidBody.velocity;
        }


        public void SetIsPressingDown(bool isPressingDown) {
            this.isPressingDown = isPressingDown;
        }

        public void SubscribeToJumpTrigger(Action myEvent) {
            jumpTrigger.AddListener(myEvent.Invoke);
        }

        public void UnsubscribeFromJumpTrigger(Action myEvent) {
            jumpTrigger.RemoveListener(myEvent.Invoke);
        }

        public void SubscribeToLandTrigger(Action myEvent) {
            landTrigger.AddListener(myEvent.Invoke);
        }

        public void UnsubscribeFromLandTrigger(Action myEvent) {
            landTrigger.RemoveListener(myEvent.Invoke);
        }
    }
}


