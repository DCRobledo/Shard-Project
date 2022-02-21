using Shard.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : EntityMovement
{
    public override void Move() { 
        float x = isFacingRight ? 1 : -1;

        // Compute the new target velocity, smooth it, and apply it
        Vector3 targetVelocity = new Vector2(Mathf.Round(x) * speed, rigidBody.velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, smoothingFactor);

        // Flip if necessary
        if      (x > 0 && this.gameObject.transform.localScale.x < 0) Flip();
        else if (x < 0 && this.gameObject.transform.localScale.x > 0) Flip();
    }

    public override void Jump(bool jump)  {
        if(canJump) {
            this.shouldJump = true;

            jumpTrigger?.Invoke();
        }
    } 
}
