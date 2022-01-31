using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Shard.Tests.Lib
{
    public class TransformTest
    {
        [Test]
        public void FlipObjectTest()
        {
            // Create a right-facing object
            GameObject go = new GameObject();
            go.transform.localScale = new Vector3(1,0,0);

            // Flip it
            TransformUtils.FlipObject(ref go);
            Assert.AreEqual(new Vector3(-1,0,0), go.transform.localScale);

            // Flip it again
            TransformUtils.FlipObject(ref go);
            Assert.AreEqual(new Vector3(1,0,0), go.transform.localScale);
        }
    }
}

