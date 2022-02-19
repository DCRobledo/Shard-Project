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
            Detection.Direction direction = this.transform.localScale.x >= 0 ? Detection.Direction.RIGHT : Detection.Direction.LEFT;

            return Detection.DetectObject(boxCollider2D, sensorDetectionLayer, direction, 0.5f);
        }

        public string CheckBehind() {
            Detection.Direction direction = this.transform.localScale.x >= 0 ? Detection.Direction.LEFT : Detection.Direction.RIGHT;

            return Detection.DetectObject(boxCollider2D, sensorDetectionLayer, direction, 0.5f);
        }

        public string CheckBelow() {
            return Detection.DetectObject(boxCollider2D, sensorDetectionLayer, Detection.Direction.DOWN, 0.5f);
        }

        public string CheckAbove() {
            return Detection.DetectObject(boxCollider2D, sensorDetectionLayer, Detection.Direction.UP, 0.5f);
        }
    }
}

