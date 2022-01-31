using Shard.Classes.Patterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Shard.Tests.Patterns
{
    public class SingletonTest
    {
        [UnityTest]
        public IEnumerator SingleInstanceTest()
        {
            // Create two singletons
            GameObject go = new GameObject();
            go.AddComponent<SingletonUnity>();

            GameObject go_ = new GameObject();
            go_.AddComponent<SingletonUnity>();

            // Get two instances
            SingletonUnity instance = SingletonUnity.Instance;
            SingletonUnity instance_ = SingletonUnity.Instance;

            // Check that they are the same
            LogAssert.Expect(LogType.Warning, "You have more than one singleton instance in the scene!");
            Assert.AreEqual(instance.id, instance_.id);

            yield return null;
        }
    }
}

