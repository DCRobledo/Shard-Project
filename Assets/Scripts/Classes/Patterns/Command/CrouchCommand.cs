using Shard.Classes.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public class CrouchCommand : Command
    {
        private EntityMovement entityMovement;


        public CrouchCommand(EntityMovement entityMovement)
        {
            this.entityMovement = entityMovement;
        }

        public override void ExecuteWithParameters(params object[] parameters)
        {
            entityMovement.Crouch((bool) parameters[0]);
        }

        public override void Execute() { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}
