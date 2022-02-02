using Shard.Lib.Custom;
using Shard.Classes.Entities;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Shard.Tests
{
    public abstract class PlayModeTest
    {
        [SetUp]
        protected void SetUp() {
            Clean.CleanGameObjects();
        }
    }
}


