using Shard.Monobehaviour.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public class ReCallCommand : Command
    {
        private EntityActions entityActions;


        public ReCallCommand(EntityActions entityActions)
        {
            this.entityActions = entityActions;
        }
        
        public override void Execute() { entityActions.ReCall(); }

        public override void ExecuteWithParameters(params object[] parameters) { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}

