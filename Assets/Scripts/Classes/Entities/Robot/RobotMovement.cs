using Shard.Controllers;
using System;
using System.Collections;
using UnityEngine;

namespace Shard.Entities
{
    public class RobotMovement : EntityMovement
    {
        public static Action moveTrigger;
        public static Action stopTrigger;
        public static Action flipTrigger;

        public bool isMoving;

        private void Update() {
            Vector2 velocity = GetComponent<RobotMovement>().GetVelocity();

            if (isMoving && velocity.x <= 0.01f && velocity.x >= -0.01f)
            {
                isMoving = false;
                stopTrigger?.Invoke();
            }
        }

        public override void Move() { 
            if (RobotController.Instance.IsRobotOn() && !RobotController.Instance.IsRobotGrabbed())
            {
                moveTrigger?.Invoke();

                isMoving = true;

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
