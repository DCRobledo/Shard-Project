using Shard.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Patterns.Command 
{
    public class ProgramCommand : Command
    {
        public override void Execute() { 
            UIController.Instance.ToggleProgrammingUI();
        }

        public override void ExecuteWithParameters(params object[] parameters) { throw new System.NotImplementedException(); }
        public override void Undo() { throw new System.NotImplementedException(); }
    }
}
