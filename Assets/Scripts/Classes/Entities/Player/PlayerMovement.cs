using Shard.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class PlayerMovement : EntityMovement
    {
        public override void Move(float x, float y) 
        {
            // Compute the new target velocity, smooth it, and apply it
            Vector3 targetVelocity = new Vector2(Mathf.Round(x) * speed, rigidBody.velocity.y);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, smoothingFactor);

            // Flip if necessary
            if      (x > 0 && this.gameObject.transform.localScale.x < 0) Flip();
            else if (x < 0 && this.gameObject.transform.localScale.x > 0) Flip();
        }

        public override void Jump(bool jump) 
        {
            // Realising the jump button is always allowed
            if(!jump) this.shouldJump = jump;

            // But we want to delay the jumps between one another
            else if(jump && canJump) 
                this.shouldJump = jump;
        } 
    }
}