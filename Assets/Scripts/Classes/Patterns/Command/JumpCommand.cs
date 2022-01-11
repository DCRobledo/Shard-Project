using Shard.Monobehaviour.Entities.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public class JumpCommand : Command
    {
        private EntityMovement entityMovement;

        public override void Execute()
        {
            entityMovement.Jump();
        }

        public override void Undo() {}
    }
}


