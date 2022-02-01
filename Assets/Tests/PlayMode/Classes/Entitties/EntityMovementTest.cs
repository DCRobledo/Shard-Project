using Shard.Lib.Custom;
using Shard.Classes.Entities;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Shard.Tests.Entities
{
    public class EntityMovementTest
    {
        [UnityTest]
        public IEnumerator EntityMoveTest()
        {
            // Create player
            GameObject player = InstantiateUtils.InstantiatePlayer(new Vector3(0, 0, 0), false);

            // Move
            player.GetComponent<EntityMovement>().Move(10f, 0f);

            // Wait for move to end
            yield return new WaitForSeconds(1f);

            // Check that positions differs in correct direction
            Assert.IsTrue(player.transform.position.x > 0);
        }

        [UnityTest]
        public IEnumerator EntityJumpTest()
        {

            yield return null;
        }

        [UnityTest]
        public IEnumerator EntityCrouchTest()
        {

            yield return null;
        }
    }
}
