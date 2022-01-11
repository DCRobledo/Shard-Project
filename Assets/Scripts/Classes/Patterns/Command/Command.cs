using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Command 
{
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void ExecuteWithParameter<ParameterType>(ParameterType parameter);

        public abstract void Undo();
    }
}

