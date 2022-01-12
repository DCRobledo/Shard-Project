using Shard.Monobehaviour.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public class JumpCommand : Command
    {
        private EntityMovement entityMovement;


        public JumpCommand(EntityMovement entityMovement)
        {
            this.entityMovement = entityMovement;
        }

        public override void Execute()
        {
            entityMovement.Jump();
        }

        public override void ExecuteWithParameters(params object[] parameters) { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}


