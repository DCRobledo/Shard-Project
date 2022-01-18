using Shard.Monobehaviour.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public class GrabCommand : Command
    {
        private EntityActions entityActions;


        public GrabCommand(EntityActions entityActions)
        {
            this.entityActions = entityActions;
        }
        
        public override void Execute() { entityActions.Grab(); }

        public override void ExecuteWithParameters(params object[] parameters) { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}
