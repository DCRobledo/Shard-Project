using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public class DoNothingCommand : Command
    {
        public override void Execute() {}
        public override void ExecuteWithParameters(params object[] parameters) {}

        public override void Undo() {}
    }
}
