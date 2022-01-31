using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Shard.Tests.Lib
{
    public class DetectionTest
    {
        private GameObject CreateGameObject(Vector3 position, int layer, string tag = null) {
            GameObject go = new GameObject();
            go.AddComponent<BoxCollider2D>();

            go.transform.position = position;
            go.layer = layer;
            if(tag != null) go.transform.tag = tag;

            return go;
        }

        [Test]
        public void DetectNearObjectsWithLayerTest()
        {
            // Create central object
            GameObject centralGO = CreateGameObject(new Vector3(0f, 0f, 0f), 1);

            // Create near object with target layermask
            GameObject nearGOWithLayer = CreateGameObject(new Vector3(1f, 0f, 0f), 2);

            // Create near object without target layermask
            GameObject nearGOWithoutLayer = CreateGameObject(new Vector3(-1f, 0f, 0f), 1);

            // Create far object with target layermask
            GameObject farGO = CreateGameObject(new Vector3(-5f, 0f, 0f), 2);


            // Register near objects
            List<GameObject> nearGOS = Detection.DetectNearObjects(centralGO.transform.position, 4f, 2);
            foreach (GameObject go in nearGOS)
                Debug.Log(go.transform.position);
                
            

            // Check that only the closer one got registered
            Assert.AreEqual(1, nearGOS.Count);
        }

        [Test]
        public void DetectNearObjectsWithTagTest()
        {
            // Create central object
            GameObject centralGO = CreateGameObject(new Vector3(0f, 0f, 0f), 1);

            // Create near object with target tag
            GameObject nearGOWithTag = CreateGameObject(new Vector3(1f, 0f, 0f), 1, "Player");

            // Create near object without target tag
            GameObject nearGOWithoutTag = CreateGameObject(new Vector3(-1f, 0f, 0f), 1);

            // Create far object with target tag
            GameObject farGO = CreateGameObject(new Vector3(-5f, 0f, 0f), 1, "Player");


            // Register near objects
            string[] tags =  {"Player"};
            List<GameObject> nearGOS = Detection.DetectNearObjects(centralGO.transform.position, 4f, tags);
            foreach (GameObject go in nearGOS)
                Debug.Log(go.transform.position);

            // Check that only the closer one got registered
            Assert.AreEqual(1, nearGOS.Count);
        }

        [Test]
        public void DetectGroundTest()
        {
            
        }
    }
}

