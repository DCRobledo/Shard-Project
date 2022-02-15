using Shard.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Patterns.Command 
{
    public class FlipCommand : Command
    {
        private EntityMovement entityMovement;


        public FlipCommand(EntityMovement entityMovement)
        {
            this.entityMovement = entityMovement;
        }

        public override void Execute() {
            entityMovement.Flip();
        }

        public override void ExecuteWithParameters(params object[] parameters) { throw new System.NotImplementedException(); }
        
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}