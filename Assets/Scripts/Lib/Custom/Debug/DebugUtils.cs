using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Lib.Custom
{
    public static class DebugUtils
    {
        public static void DebugBoxRayCast(Vector3 center, Vector2 direction, float xLimit, float yLimit, Color color) {
            Debug.DrawRay(center + new Vector3(xLimit, 0f), direction * (yLimit), color);
            Debug.DrawRay(center - new Vector3(xLimit, 0f), direction * (yLimit), color);
            Debug.DrawRay(center - new Vector3(xLimit, yLimit), new Vector2(direction.x + 1, direction.y + 1) * (xLimit * 2), color);
        }
    }
}


