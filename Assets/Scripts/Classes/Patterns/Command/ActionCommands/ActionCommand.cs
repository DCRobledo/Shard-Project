using Shard.Entities;
using Shard.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Patterns.Command 
{
    public class ActionCommand : Command
    {
        private EntityActions entityActions;


        public ActionCommand(EntityActions entityActions)
        {
            this.entityActions = entityActions;
        }
    
        public override void ExecuteWithParameters(params object[] parameters) { 
            
            entityActions.ExecuteAction((EntityEnum.Action) parameters[0]); 
        }

        public override void Execute() { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}
