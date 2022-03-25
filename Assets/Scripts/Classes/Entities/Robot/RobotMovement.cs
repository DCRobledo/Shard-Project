using Shard.Controllers;

namespace Shard.Entities
{
    public class RobotMovement : EntityMovement
    {
        public override void Move() { 
            if (RobotController.Instance.IsRobotOn() && !RobotController.Instance.IsRobotGrabbed())
            {
                float x = isFacingRight ? 1 : -1;

                Move(x, rigidBody.velocity.y);
            }
        }

        public override void Jump()  {
            if (RobotController.Instance.IsRobotOn())
                Jump(true);
        } 
    }
}
