using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public class PlayerMovement : EntityMovement
    {
        public override void Jump() 
        {
            Debug.Log("Jump!");
        }

        public override void Move<Vector2>(Vector2 direction) 
        {
            Debug.Log(direction);
        }
    }
}


