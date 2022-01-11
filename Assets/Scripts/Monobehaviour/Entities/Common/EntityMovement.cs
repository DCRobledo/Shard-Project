using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public abstract class EntityMovement : MonoBehaviour
    {
        public abstract void Move<Vector2>(Vector2 direction);

        public abstract void Jump();
    }
}


