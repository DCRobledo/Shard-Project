using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Lib.Custom
{
    public static class VisualUtils
    {
        public static void ChangeObjectColor(ref GameObject gameObject, float r, float g, float b)
        {
            Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
            newColor.r += r; newColor.g += g; newColor.b += b;
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        public static void ChangeObjectsColor(ref List<GameObject> gameObjects, float r, float g, float b)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
                newColor.r += r; newColor.g += g; newColor.b += b;
                gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
        }
    }
}
