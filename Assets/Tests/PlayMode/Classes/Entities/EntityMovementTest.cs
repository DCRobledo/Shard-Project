using Shard.Lib.Custom;
using Shard.Classes.Entities;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Shard.Tests.Entities
{
    public class EntityMovementTest
    {
        [UnityTest]
        public IEnumerator EntityMoveTest()
        {
            Clean.CleanGameObjects();

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
            Clean.CleanGameObjects();

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
            // Create two players
            GameObject playerCrouch = InstantiateUtils.InstantiatePlayer(new Vector3(-5f, 0, 0));
            GameObject playerNotCrouch = InstantiateUtils.InstantiatePlayer(new Vector3(5f, 0, 0));

            // Create a platform
            InstantiateUtils.InstantiatePlatform(new Vector3(0, -4f, 0), 100f, 4f);

            // Wait for landing
            yield return new WaitForSeconds(.8f);


            // Make one of them crouch
            Vector3 originalScale = playerCrouch.transform.localScale;
            playerCrouch.GetComponent<EntityMovement>().Crouch(true);

            // Check the crouching one's scale
            Assert.IsTrue(playerCrouch.transform.localScale.y < originalScale.y);


            // Move one player while crouching and the other one without crouching
            Vector3 originalPositionCrouch = playerCrouch.transform.position;
            Vector3 originalPositionNotCrouch = playerNotCrouch.transform.position;

            playerCrouch.GetComponent<EntityMovement>().Move(5f, 0);
            playerNotCrouch.GetComponent<EntityMovement>().Move(5f, 0);

            // Wait for the movement
            yield return new WaitForSeconds(1f);

            // Check that the crouching one has covered less distance
            float crouchDistance    = playerCrouch.transform.position.x - originalPositionCrouch.x;
            float notCrouchDistance = playerNotCrouch.transform.position.x - originalPositionNotCrouch.x;

            Assert.IsTrue(crouchDistance < notCrouchDistance);

            yield return null;
        }
    }
}
