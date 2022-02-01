using Shard.Lib.Custom;
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
        [UnityTest]
        public IEnumerator EntityGrabTest()
        {
            // Create the player
            GameObject player = InstantiateUtils.InstantiatePlayer(new Vector3(0, 0, 0), false);

            // Create a robot
            GameObject robot = InstantiateUtils.InstantiateRobot(new Vector3(1f, 0, 0), false);


            // Wait for grabbable objects checking
            yield return new WaitForSeconds(.2f);


            // Grab robot
            player.GetComponent<EntityActions>().Grab();

            // Wait for grab
            yield return new WaitForSeconds(.2f);

            // Check that it's been grabbed
            Assert.AreSame(robot.GetComponent<Rigidbody2D>(), player.GetComponent<RelativeJoint2D>().connectedBody);


            // Release it
            player.GetComponent<EntityActions>().Grab();

            // Wait for release
            yield return new WaitForSeconds(.2f);

            // Check that it's been released
            Assert.IsNull(player.GetComponent<RelativeJoint2D>().connectedBody);


            // Move it out of grab range
            robot.transform.position = new Vector3(5f, 0, 0);

            // Wait for grab
            yield return new WaitForSeconds(.2f);

            // Try to grab it
            player.GetComponent<EntityActions>().Grab();


            // Check that it hasn't been grabbed again
            Assert.IsNull(player.GetComponent<RelativeJoint2D>().connectedBody);
        }

        [UnityTest]
        public IEnumerator EntityReCallTest()
        {
            // Create player
            GameObject player = InstantiateUtils.InstantiatePlayer(new Vector3(0, 0, 0), false);

            // Create robot
            GameObject robot = InstantiateUtils.InstantiateRobot(new Vector3(10f, 0, 0), false);


            // Check that it is far
            Assert.AreEqual(0, Detection.DetectNearObjects(player.transform.position, 5f, 1).Count);

            // ReCall robot
            player.GetComponent<EntityActions>().ChangeObjectToReCall(robot);
            player.GetComponent<EntityActions>().ReCall();


            // Check that it is near
            Assert.AreEqual(0, Detection.DetectNearObjects(player.transform.position, 5f, 1).Count);


            yield return null;
        }
    }
}


