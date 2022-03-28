using Shard.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class PlayerMovement : EntityMovement
    {
        public override void Flip() {
            base.Flip();

            Debug.Log("child");
        }
    }
}