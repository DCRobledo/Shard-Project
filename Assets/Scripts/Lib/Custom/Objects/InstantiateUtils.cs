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

            go.GetComponent<Rigidbody2D>().bodyType = applyGravity ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
            go.transform.position = position;

            return go;
        }

        public static GameObject InstantiatePlayer(Vector3 position, bool applyGravity = true) { return InstantiateObject("Entities/player.prefab", position, applyGravity); }

        public static GameObject InstantiateRobot(Vector3 position, bool applyGravity = true) { return InstantiateObject("Entities/robot.prefab", position, applyGravity); }
    }
}
