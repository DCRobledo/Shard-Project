using Shard.Monobehaviour.Entities.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public class JumpCommand : Command
    {
        private PlayerMovement playerMovement;

        public JumpCommand(PlayerMovement playerMovement)
        {
            this.playerMovement = playerMovement;
        }

        public override void Execute()
        {
            playerMovement.Jump();
        }

        public override void Undo() {}
    }
}


