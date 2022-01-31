using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Entities
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f;
        [SerializeField]
        private float jumpForce = 12f;
        [SerializeField] [Range(1, 10)]
        private float fallMultiplier = 2.5f;
        [SerializeField] [Range(1, 10)]
        private float lowJumpMultiplier = 2f;
        [SerializeField] [Range(1, 10)]
        private float crouchFactor = 2f;
        

        [SerializeField]
        private LayerMask whatIsGround;

        [SerializeField] [Range(0, .3f)]
        private float smoothingFactor = .05f;

        private Vector3 velocity = Vector3.zero;

        private Rigidbody2D rigidBody;
        private BoxCollider2D boxCollider2D; 

        private bool canJump = true;
        private bool shouldJump = false;


        private void Awake() 
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
            this.boxCollider2D =  this.GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate() {
            // Jump if requested
            if (IsGrounded() && shouldJump)
                rigidBody.velocity += Vector2.up * jumpForce;

            ApplyGravity(this.fallMultiplier, this.lowJumpMultiplier);
        }


        public void Move(float x, float y) 
        {
            // Compute the new target velocity, smooth it, and apply it
            Vector3 targetVelocity = new Vector2(Mathf.Round(x) * speed, rigidBody.velocity.y);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, smoothingFactor);

            // Flip if necessary
            if      (x > 0 && this.gameObject.transform.localScale.x < 0) Flip();
            else if (x < 0 && this.gameObject.transform.localScale.x > 0) Flip();
        }

        private void Flip() {
            // Flip the entity
            GameObject entity = this.gameObject;
            TransformUtils.FlipObject(ref entity);

            // Flip the anchor point of grab
            this.GetComponent<RelativeJoint2D>().linearOffset = new Vector2(this.GetComponent<RelativeJoint2D>().linearOffset.x * -1, this.GetComponent<RelativeJoint2D>().linearOffset.y);
        
            // Flip the grabbed entity if it exists
            GameObject grabbedEntity = this.GetComponent<RelativeJoint2D>().connectedBody?.gameObject;
            if(grabbedEntity != null) TransformUtils.FlipObject(ref grabbedEntity);
        }


        public void Jump(bool jump) 
        {
            // Realising the jump button is always allowed
            if(!jump) this.shouldJump = jump;

            // But we want to delay the jumps between one another
            else if(jump && canJump) {
                this.shouldJump = jump;

                //StartCoroutine(TimerOnJump(jumpDelay));
            }
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
            return Detection.DetectGround(this.boxCollider2D, this.whatIsGround, .25f, true);
        }

        private IEnumerator TimerOnJump(float seconds) {
            canJump = false;

            yield return new WaitForSeconds(seconds);

            canJump = true;
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


