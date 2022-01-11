using Shard.Monobehaviour.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public class MoveCommand : Command
    {
        private EntityMovement entityMovement;


        public MoveCommand(EntityMovement entityMovement)
        {
            this.entityMovement = entityMovement;
        }

        public override void ExecuteWithParameter<Vector2>(Vector2 direction)
        {
            entityMovement.Move<Vector2>(direction);
        }

        public override void Execute() { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}