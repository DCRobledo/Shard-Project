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
            // Create player
            GameObject player = InstantiateUtils.InstantiatePlayer(new Vector3(0, 0, 0));

            // Create platform
            InstantiateUtils.InstantiatePlatform(new Vector3(0, -4f, 0), 8f, 4f);

            //  Wait for the player to land
            yield return new WaitForSeconds(1f);

            // Check grounded position
            Vector3 groundedPosition = player.transform.position;


            // Jump
            player.GetComponent<EntityMovement>().Jump(true);

            // Wait for half the jump
            yield return new WaitForSeconds (.5f);

            // Check mid-air position
            Assert.True(player.transform.position.y > groundedPosition.y);


            // Wait for other half
            yield return new WaitForSeconds (.5f);

            // Check landing position
            Assert.AreEqual(groundedPosition, player.transform.position);
        }

        [UnityTest]
        public IEnumerator EntityCrouchTest()
        {

            yield return null;
        }
    }
}
