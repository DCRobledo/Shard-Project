using Shard.Assets.Scripts.Monobehaviour.Entities.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Assets.Scripts.Classes.Patterns.Command 
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


