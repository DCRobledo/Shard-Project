using Shard.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow
{
    public class ReCallZone : MonoBehaviour
    {
        public void ReCallRobot() {
            GameObject.FindGameObjectWithTag("Robot").transform.position = this.transform.position;

            UIController.Instance.TurnOffButtonClick();
        }
    }
}


