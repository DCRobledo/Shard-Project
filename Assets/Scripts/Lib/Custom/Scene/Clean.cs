using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Lib.Custom
{
    public static class Clean
    {
        public static void CleanGameObjects() {
            GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();

            if(gos.Length > 0) foreach (GameObject go in gos) Object.Destroy(go.gameObject);
        }
    }
}


