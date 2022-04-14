using Shard.Controllers;
using System;

namespace Shard.Entities
{
    public class RobotMovement : EntityMovement
    {
        public static Action moveTrigger;
        public static Action flipTrigger;

        public override void Move() { 
            if (RobotController.Instance.IsRobotOn() && !RobotController.Instance.IsRobotGrabbed())
            {
                moveTrigger?.Invoke();

                float x = isFacingRight ? 1 : -1;

                Move(x, rigidBody.velocity.y);
            }
        }

        public override void Jump()  {
            if (RobotController.Instance.IsRobotOn()) {
                // We want the robot to jump from the player's arms
                if(RobotController.Instance.IsRobotGrabbed())
                {
                    PlayerController.Instance.ReleaseRobot();

                    this.canJump = true;
                }

                Jump(true);
            }
                
        }

        public override void Flip()
        {
            flipTrigger?.Invoke();

            base.Flip();
        }
    }
}
