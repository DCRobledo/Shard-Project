using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Utils
{
    public static class DebugUtils
    {
        public static void DebugBoxRayCast(Vector3 center, float xLimit, float yLimit, Color color) {
            Debug.DrawRay(center + new Vector3(xLimit, 0f), Vector2.down * (yLimit), color);
            Debug.DrawRay(center - new Vector3(xLimit, 0f), Vector2.down * (yLimit), color);
            Debug.DrawRay(center - new Vector3(xLimit, yLimit), Vector2.right * (xLimit * 2), color);
        }
    }
}


