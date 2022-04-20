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

        private PolygonCollider2D polygonCollider2D;


        private void Awake() {
            polygonCollider2D = this.GetComponent<PolygonCollider2D>();
        }


        public string CheckAhead() {
            Detection.Direction direction = this.transform.localScale.x >= 0 ? Detection.Direction.RIGHT : Detection.Direction.LEFT;

            return Detection.DetectObject(polygonCollider2D, sensorDetectionLayer, direction, polygonCollider2D.bounds.size, 0.5f, "Void");
        }

        public string CheckBehind() {
            Detection.Direction direction = this.transform.localScale.x >= 0 ? Detection.Direction.LEFT : Detection.Direction.RIGHT;

            return Detection.DetectObject(polygonCollider2D, sensorDetectionLayer, direction, polygonCollider2D.bounds.size, 0.5f, "Void");
        }

        public string CheckBelow() {
            return Detection.DetectObject(polygonCollider2D, sensorDetectionLayer, Detection.Direction.DOWN, defaultResult:"Void");
        }

        public string CheckAbove() {
            return Detection.DetectObject(polygonCollider2D, sensorDetectionLayer, Detection.Direction.UP, 2f, "Void", true);
        }
    }
}


