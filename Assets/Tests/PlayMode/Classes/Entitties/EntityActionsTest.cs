using Shard.Classes.Entities;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Shard.Tests.Entities
{
    public class EntityActionsTest
    {
        private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefabs/Entities/player.prefab");
        private GameObject robotPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefabs/Entities/robot.prefab");

        [UnityTest]
        public IEnumerator EntityGrabTest()
        {
            // Create the player
            GameObject player = Object.Instantiate(playerPrefab as GameObject);
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.transform.position = new Vector3(0, 0, 0);

            // Create a robot
            GameObject robot = Object.Instantiate(robotPrefab as GameObject);
            robot.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            robot.transform.position = new Vector3(1f, 0, 0);


            // Wait for grabbable objects checking
            yield return new WaitForSeconds(.5f);


            // Grab robot
            player.GetComponent<EntityActions>().Grab();

            // Wait for grab
            yield return new WaitForSeconds(1f);

            // Check that it's been grabbed
            Assert.AreSame(robot.GetComponent<Rigidbody2D>(), player.GetComponent<RelativeJoint2D>().connectedBody);


            // Release it
            player.GetComponent<EntityActions>().Grab();

            // Wait for release
            yield return new WaitForSeconds(1f);

            // Check that it's been released
            Assert.IsNull(player.GetComponent<RelativeJoint2D>().connectedBody);


            // Move it out of grab range
            robot.transform.position = new Vector3(5f, 0, 0);

            // Wait for grab
            yield return new WaitForSeconds(1f);

            // Try to grab it
            player.GetComponent<EntityActions>().Grab();


            // Check that it hasn't been grabbed again
            Assert.IsNull(player.GetComponent<RelativeJoint2D>().connectedBody);

            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerReCallTest()
        {
            
            yield return null;
        }
    }
}


