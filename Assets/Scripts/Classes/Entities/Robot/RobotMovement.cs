using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class RobotMovement : EntityMovement
    {
        public override void Move() { 
            float x = isFacingRight ? 1 : -1;

            Move(x, rigidBody.velocity.y);
        }

        public override void Jump()  {
            Jump(true);
        } 
    }
}
