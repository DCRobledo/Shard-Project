using Shard.Gameflow;
using Shard.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Patterns.Command 
{
    public class ReturnToCheckpointCommand : Command
    {
        private EntityEnum.Entity entity;

        public ReturnToCheckpointCommand(EntityEnum.Entity entity) {
            this.entity = entity;
        }

        public override void Execute() { 
             CheckPointsManagement.returnToLastCheckpointEvent?.Invoke(entity);
        }

        public override void ExecuteWithParameters(params object[] parameters) { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}
