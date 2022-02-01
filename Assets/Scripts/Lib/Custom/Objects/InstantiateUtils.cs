using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Shard.Lib.Custom
{
    public static class InstantiateUtils
    {
        public static GameObject InstantiateObject(string path, Vector3 position, bool applyGravity = true) {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefabs/" + path);
            GameObject go = Object.Instantiate(prefab as GameObject);

            if(!applyGravity) go.GetComponent<Rigidbody2D>().gravityScale = 0;
            go.transform.position = position;

            return go;
        }

        public static GameObject InstantiatePlayer(Vector3 position, bool applyGravity = true) { return InstantiateObject("Entities/player.prefab", position, applyGravity); }

        public static GameObject InstantiateRobot(Vector3 position, bool applyGravity = true) { return InstantiateObject("Entities/robot.prefab", position, applyGravity); }
    

        public static GameObject InstantiatePlatform(Vector3 position, float width, float height) {
            GameObject platform = new GameObject();

            platform.transform.position = position;
            platform.transform.localScale =  new Vector3(width, height, 1f);
            platform.layer = 6;

            platform.AddComponent<SpriteRenderer>();
            platform.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Materials/Images/Tile_PH.jpg");

            platform.AddComponent<BoxCollider2D>();

            return Object.Instantiate(platform as GameObject);
        }
    }
}
