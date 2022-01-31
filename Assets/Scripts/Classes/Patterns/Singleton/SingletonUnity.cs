using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Classes.Patterns.Singleton
{
    public class SingletonUnity : MonoBehaviour
    {
        public float id;
        
        private static SingletonUnity instance = null;

        public static SingletonUnity Instance {
            get {
                // This means that we have one (or more) instances in the scene, now we need to make sure to only pick one
                if (instance == null) {
                    SingletonUnity[] singletonsInScene = GameObject.FindObjectsOfType<SingletonUnity>();

                    // Ensure there is only one instance in the scene
                    if (singletonsInScene != null) {

                        if (singletonsInScene.Length > 1) {
                            Debug.LogWarning("You have more than one singleton instance in the scene!");

                            for (int i = 0; i < singletonsInScene.Length; i++)
                                Destroy(singletonsInScene[i].gameObject);
                        }

                        // Take the single instance
                        instance = singletonsInScene[0];

                        // Initiate it
                        instance.Init();
                    }
                }

                return instance;
            }
        }

        private void Init() {
            id = Random.Range(0, 50);
        }

        public void TestSingleton() {
            Debug.Log($"Hello this is Singleton, my id number is: {id}");
        }
    }
}


