using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Lib.Custom
{
    public static class Detection
    {
        public enum Direction {
            RIGHT,
            LEFT,
            UP,
            DOWN
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
                    Vector2.down,
                    boxCollider2D.bounds.extents.x,
                    boxCollider2D.bounds.extents.y + offset,
                    rayColor
                );
            }
            

            return isGrounded;
        }

        public static string DetectObject(BoxCollider2D boxCollider2D, LayerMask layerMask, Direction direction, Vector3 size, float offset, string defaultResult = null) {
            float xDirection = direction == Direction.RIGHT ? 1f : (direction == Direction.LEFT ? -1f : 0f);
            float yDirection = direction == Direction.UP ? 1f : (direction == Direction.DOWN ? -1f : 0f);

            Vector2 castDirection = new Vector2(xDirection, yDirection);

            RaycastHit2D rayCastHit = 
            Physics2D.BoxCast(
                boxCollider2D.bounds.center,
                size,
                0f,
                castDirection,
                offset,
                layerMask
            );      

            return rayCastHit.collider != null ? rayCastHit.collider.transform.tag : defaultResult;
        }

        public static List<string> DetectObjects(BoxCollider2D boxCollider2D, LayerMask layerMask, float offset) {
            List<string> detectedObjects = new List<string>();

            RaycastHit2D[] rayCastHits = 
            Physics2D.BoxCastAll(
                boxCollider2D.bounds.center,
                boxCollider2D.bounds.size,
                0f,
                Vector2.down,
                offset,
                layerMask
            );

            if(rayCastHits.Length == 0) return null;

            foreach (RaycastHit2D raycastHit in rayCastHits)
                detectedObjects.Add(raycastHit.collider.tag);

            return detectedObjects;    
        }

        public static string DetectObject(BoxCollider2D boxCollider2D, LayerMask layerMask, Direction direction, string defaultResult = null) {
            float xDirection = direction == Direction.RIGHT ? 1f : (direction == Direction.LEFT ? -1f : 0f);
            float yDirection = direction == Direction.UP ? 1f : (direction == Direction.DOWN ? -1f : 0f);

            Vector2 castDirection = new Vector2(xDirection, yDirection);

            RaycastHit2D rayCastHit = 
            Physics2D.Raycast (
                boxCollider2D.bounds.center,
                castDirection,
                boxCollider2D.bounds.size.x + 0.5f,
                layerMask
            );    

            return rayCastHit.collider != null ? rayCastHit.collider.transform.tag : defaultResult;
        }  
    }
}
