using Shard.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Patterns.Command 
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

        public override void ExecuteWithParameters(params object[] parameters)
        {
            entityMovement.Jump((bool) parameters[0]);
        }

        public override void Undo() { throw new System.NotImplementedException(); }
    }
}


