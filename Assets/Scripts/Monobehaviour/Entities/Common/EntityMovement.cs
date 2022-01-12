using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public abstract class EntityMovement : MonoBehaviour
    {
        public abstract void Move(float x, float y);

        public abstract void Jump();
    }
}


