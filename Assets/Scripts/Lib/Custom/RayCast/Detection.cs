using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Lib.Custom
{
    public static class Detection
    {
        public enum Direction {
            RIGHT = 1,
            LEFT = -1,
            UP = 1,
            DOWN = -1
        }


        public static List<GameObject> DetectNearObjects(Vector3 center, float radius, LayerMask layerMask) {
            List<GameObject> detectedGameObjects = new List<GameObject>();

            // Check all near 
            Collider2D[] nearObjects = Physics2D.OverlapCircleAll(center, radius, layerMask);
            
            foreach (Collider2D collider in nearObjects)
                if (!detectedGameObjects.Contains(collider.gameObject))
                    detectedGameObjects.Add(collider.gameObject);

            return detectedGameObjects;
        }

        public static List<GameObject> DetectNearObjects(Vector3 center, float radius, string[] tags) {
            List<GameObject> detectedGameObjects = new List<GameObject>();

            // Check all near 
            Collider2D[] nearObjects = Physics2D.OverlapCircleAll(center, radius);
            
            // Filter by tag
            foreach (Collider2D collider in nearObjects)
                if (!detectedGameObjects.Contains(collider.gameObject) && tags.Contains(collider.gameObject.transform.tag))
                    detectedGameObjects.Add(collider.gameObject);

            return detectedGameObjects;
        }
    
        public static bool DetectGround(BoxCollider2D boxCollider2D, LayerMask whatIsGround, float offset, bool debug = false) {
            bool isGrounded;

            // Check ground through raycasting the circle collider
            RaycastHit2D rayCastHit = 
            Physics2D.BoxCast(
                boxCollider2D.bounds.center,
                boxCollider2D.bounds.size,
                0f,
                Vector2.down,
                offset,
                whatIsGround
            );      

            isGrounded = rayCastHit.collider != null;

            if (debug) {
                Color rayColor = isGrounded ? Color.green : Color.red;
                DebugUtils.DebugBoxRayCast(
                    boxCollider2D.bounds.center,
                    boxCollider2D.bounds.extents.x,
                    boxCollider2D.bounds.extents.y + offset,
                    rayColor
                );
            }
            

            return isGrounded;
        }

        public static string DetectObject(BoxCollider2D boxCollider2D, LayerMask layerMask, Direction direction, float offset, bool debug = false) {
            float xDirection = (direction == Direction.RIGHT || direction == Direction.LEFT) ? (float) direction : 0f;
            float yDirection = (direction == Direction.UP || direction == Direction.DOWN) ? (float) direction : 0f;

            Vector2 castDirection = new Vector2(xDirection, yDirection);

            RaycastHit2D rayCastHit = 
            Physics2D.BoxCast(
                boxCollider2D.bounds.center,
                boxCollider2D.bounds.size,
                0f,
                castDirection,
                offset,
                layerMask
            );      

            bool isDetecting = rayCastHit.collider != null;

            if (debug) {
                Color rayColor = isDetecting ? Color.green : Color.red;
                DebugUtils.DebugBoxRayCast(
                    boxCollider2D.bounds.center,
                    boxCollider2D.bounds.center.x + boxCollider2D.bounds.extents.x * castDirection.x + offset,
                    boxCollider2D.bounds.center.y + boxCollider2D.bounds.extents.y * castDirection.y + offset,
                    rayColor
                );
            }

            return rayCastHit.collider != null ? rayCastHit.collider.transform.tag : null;
        }  
    }
}
