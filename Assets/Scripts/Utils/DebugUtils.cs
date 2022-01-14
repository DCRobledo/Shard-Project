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

        public static void DebugCircleRayCast(Vector3 center, float radius, Color color) {
            for (float x = -.8f, y = .2f, y_ = 1.8f; x < .8f; x+=.2f, y+=.2f, y_-=.2f) {
                if(x < 0) Debug.DrawRay(center, new Vector2(x, -Mathf.Abs(y * (radius + .5f))), color);
                else      Debug.DrawRay(center, new Vector2(x, -Mathf.Abs(y_ * (radius + .5f))), color);
            }
        }
    }
}


