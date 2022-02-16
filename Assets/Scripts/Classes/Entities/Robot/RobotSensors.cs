using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities 
{
    public class RobotSensors : MonoBehaviour
    {
        [SerializeField]
        private LayerMask sensorDetectionLayer;

        private BoxCollider2D boxCollider2D;


        private void Awake() {
            boxCollider2D = this.GetComponent<BoxCollider2D>();
        }


        public string CheckAhead() {
            return Detection.DetectObject(boxCollider2D, sensorDetectionLayer, Detection.Direction.RIGHT, 0.5f);
        }

        public string CheckBehind() {
            return Detection.DetectObject(boxCollider2D, sensorDetectionLayer, Detection.Direction.LEFT, 0.5f);
        }

        public string CheckBelow() {
            return Detection.DetectObject(boxCollider2D, sensorDetectionLayer, Detection.Direction.DOWN, 0.5f);
        }

        public string CheckAbove() {
            return Detection.DetectObject(boxCollider2D, sensorDetectionLayer, Detection.Direction.UP, 0.5f);
        }
    }
}


