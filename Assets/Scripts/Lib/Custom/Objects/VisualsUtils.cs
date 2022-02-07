using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shard.Lib.Custom
{
    public static class VisualUtils
    {
        public static void ChangeObjectColor(ref GameObject gameObject, float r = 0, float g = 0, float b = 0, float a = 0)
        {
            Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
            newColor.r += r; newColor.g += g; newColor.b += b; newColor.a += a;
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        public static void ChangeObjectsColor(ref List<GameObject> gameObjects, float r = 0, float g = 0, float b = 0, float a = 0)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
                newColor.r += r; newColor.g += g; newColor.b += b; newColor.a += a;
                gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
        }

        public static void ChangeObjectImage(ref GameObject gameObject, float r = 0, float g = 0, float b = 0, float a = 0)
        {
            Color newColor = gameObject.GetComponent<Image>().color;
            newColor.r += r; newColor.g += g; newColor.b += b; newColor.a += a;
            gameObject.GetComponent<Image>().color = newColor;
        }

        public static void ChangeObjectsImage(ref List<GameObject> gameObjects, float r = 0, float g = 0, float b = 0, float a = 0)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Color newColor = gameObject.GetComponent<Image>().color;
                newColor.r += r; newColor.g += g; newColor.b += b; newColor.a += a;
                gameObject.GetComponent<Image>().color = newColor;
            }
        }
    }
}
