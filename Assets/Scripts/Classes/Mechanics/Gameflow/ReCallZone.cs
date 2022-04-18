using Shard.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class ReCallZone : MonoBehaviour
    {
        private Animator animator;

        private void Awake() {
            this.animator = this.GetComponent<Animator>();
        }

        public IEnumerator ReCallRobot() {
            GameObject robot = GameObject.Find("robot");

            // Move robot to recall zone and make it invisible
            UIController.Instance.TurnOffButtonClick();
            robot.transform.position = this.transform.parent.position;
            Color transparentColor = robot.GetComponent<SpriteRenderer>().color;
            transparentColor.a = 0f;
            robot.GetComponent<SpriteRenderer>().color = transparentColor;

            // Open animation
            this.animator.SetTrigger("open");

            // Wait for animation to finish
            yield return new WaitForSeconds(0.8f);

            // Fade in robot
            Color robotColor = robot.GetComponent<SpriteRenderer>().color;
            
            do {
                robotColor.a += 0.08f;

                robot.GetComponent<SpriteRenderer>().color = robotColor;

                yield return null;

            } while (robotColor.a < 1f);

            // Close animation
            yield return new WaitForSeconds(0.8f);
            this.animator.SetTrigger("close");
        }
    }
}


