using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Patterns.Command 
{
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void ExecuteWithParameters(params object[] parameters);

        public abstract void Undo();
    }
}

