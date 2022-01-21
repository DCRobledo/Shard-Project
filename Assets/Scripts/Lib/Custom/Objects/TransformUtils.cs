using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Lib.Custom
{
    public static class TransformUtils
    {
        public static void FlipObject(ref GameObject gameObject) {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
    }
}
