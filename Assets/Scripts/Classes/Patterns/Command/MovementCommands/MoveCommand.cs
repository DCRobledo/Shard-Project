using Shard.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Patterns.Command 
{
    public class MoveCommand : Command
    {
        private EntityMovement entityMovement;


        public MoveCommand(EntityMovement entityMovement)
        {
            this.entityMovement = entityMovement;
        }

        public override void Execute() {
            entityMovement.Move();
        }

        public override void ExecuteWithParameters(params object[] parameters)
        {
            entityMovement.Move((float) parameters[0], (float) parameters[1]);
        }
        
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}