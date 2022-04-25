using Shard.Gameflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Patterns.Command 
{
    public class PauseCommand : Command
    {
        public override void Execute() { 
            GameFlowManagement.Pause();
        }

        public override void ExecuteWithParameters(params object[] parameters) { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}
