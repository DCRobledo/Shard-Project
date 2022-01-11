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
    }
}


