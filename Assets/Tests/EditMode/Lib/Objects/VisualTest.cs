using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Shard.Tests.Lib
{
    public class VisualTest
    {
        [Test]
        public void ChangeObjectColorTest()
        {
            GameObject go = new GameObject();
            go.AddComponent<SpriteRenderer>();

            Color originalColor = go.GetComponent<SpriteRenderer>().color;
            VisualUtils.ChangeObjectColor(ref go, .2f, .2f, .2f);
            Color modifiedColor = go.GetComponent<SpriteRenderer>().color;

            Assert.AreNotEqual(originalColor, modifiedColor);
        }

        [Test]
        public void ChangeObjectColorsTest()
        {
            List<GameObject> gos = new List<GameObject>();

            for(int i = 0; i < 10; i++) {
                GameObject go = new GameObject();
                go.AddComponent<SpriteRenderer>();
                gos.Add(go);
            }
            
            VisualUtils.ChangeObjectsColor(ref gos, .2f, .2f, .2f);

            BitArray expected = new BitArray(10, true);
            BitArray actual = new BitArray(10, false);

            for(int i = 0; i < 10; i++) 
                actual[i] = gos[i].GetComponent<SpriteRenderer>().color != Color.white;
            
            Assert.AreEqual(expected, actual);
        }
    }
}


