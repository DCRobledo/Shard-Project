using Shard.Controllers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities
{
    public class PlayerMovement : EntityMovement
    {
        public static Action flipTrigger;
        public static Action crouchTrigger;

        private new void FixedUpdate() {
            if(isCrouched) crouchTrigger?.Invoke();

            base.FixedUpdate();
        }

        public override void Flip() {
            flipTrigger?.Invoke();

            base.Flip();
        }

        // public override void Drop()
        // {
        //     if(PlayerController.Instance.IsPressingDown() && CheckPlatformBelow())
        //         base.Drop();
        // }

        // private bool CheckPlatformBelow() {
        //     return true;
        // }
    }
}