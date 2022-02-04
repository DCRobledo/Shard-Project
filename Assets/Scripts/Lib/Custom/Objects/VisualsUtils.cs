using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shard.Lib.Custom
{
    public static class VisualUtils
    {
        public static void ChangeObjectColor(ref GameObject gameObject, float r, float g, float b, float a = 0)
        {
            Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
            newColor.r += r; newColor.g += g; newColor.b += b; newColor.a += 0;
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        public static void ChangeObjectsColor(ref List<GameObject> gameObjects, float r, float g, float b, float a = 0)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
                newColor.r += r; newColor.g += g; newColor.b += b; newColor.a += 0;
                gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
        }

        public static void ChangeObjectAlpha(ref GameObject gameObject, float a)
        {
            Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
            newColor.a += a;
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        public static void ChangeImageAlpha(ref GameObject gameObject, float a)
        {
            Color newColor = gameObject.GetComponent<Image>().color;
            newColor.a += a;
            gameObject.GetComponent<Image>().color = newColor;
        }
    }
}
