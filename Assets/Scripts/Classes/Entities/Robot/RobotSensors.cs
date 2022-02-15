using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities 
{
    public static class RobotSensors
    {
        private static BoxCollider2D boxCollider2D;

        private static LayerMask layerMask;

        public static void SetBoxCollider2D(BoxCollider2D boxCollider2D) { RobotSensors.boxCollider2D = boxCollider2D; }
        public static void SetLayerMask(LayerMask layerMask) { RobotSensors.layerMask = layerMask; }

        public static string CheckAhead() {
            Debug.Log("AHEAD");
            return Detection.DetectObject(RobotSensors.boxCollider2D, layerMask, Detection.Direction.RIGHT, 0.5f, true);
        }

        public static string CheckBehind() {
            Debug.Log("BEHIND");
            return Detection.DetectObject(RobotSensors.boxCollider2D, layerMask, Detection.Direction.LEFT, 0.5f, true);
        }

        public static string CheckBelow() {
            Debug.Log("BELOW");
            return Detection.DetectObject(RobotSensors.boxCollider2D, layerMask, Detection.Direction.DOWN, 0.5f, true);
        }

        public static string CheckAbove() {
            Debug.Log("ABOVE");
            return Detection.DetectObject(RobotSensors.boxCollider2D, layerMask, Detection.Direction.UP, 0.5f, true);
        }
    }
}


