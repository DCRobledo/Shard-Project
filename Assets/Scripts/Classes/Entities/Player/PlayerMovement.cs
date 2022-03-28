using Shard.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class PlayerMovement : EntityMovement
    {
        public static Action flipTrigger;
        public static Action crouchTrigger;

        public override void Flip() {
            flipTrigger?.Invoke();

            base.Flip();
        }

        public override void Crouch(bool crouch)
        {
            if(crouch) crouchTrigger?.Invoke();
            
            base.Crouch(crouch);
        }
    }
}