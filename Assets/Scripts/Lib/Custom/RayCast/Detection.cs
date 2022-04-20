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
    
        public static bool DetectGround(PolygonCollider2D polygonCollider2D, LayerMask whatIsGround, float offset, bool debug = false) {
            bool isGrounded;

            // Check ground through raycasting the circle collider
            RaycastHit2D rayCastHit = 
            Physics2D.BoxCast(
                polygonCollider2D.bounds.center,
                polygonCollider2D.bounds.size,
                0f,
                Vector2.down,
                offset,
                whatIsGround
            );      

            isGrounded = rayCastHit.collider != null;

            if (debug) {
                Color rayColor = isGrounded ? Color.green : Color.red;
                DebugUtils.DebugBoxRayCast(
                    polygonCollider2D.bounds.center,
                    Vector2.down,
                    polygonCollider2D.bounds.extents.x,
                    polygonCollider2D.bounds.extents.y + offset,
                    rayColor
                );
            }
            

            return isGrounded;
        }

        public static string DetectObject(PolygonCollider2D polygonCollider2D, LayerMask layerMask, Direction direction, Vector3 size, float offset, string defaultResult = null) {
            float xDirection = direction == Direction.RIGHT ? 1f : (direction == Direction.LEFT ? -1f : 0f);
            float yDirection = direction == Direction.UP ? 1f : (direction == Direction.DOWN ? -1f : 0f);

            Vector2 castDirection = new Vector2(xDirection, yDirection);

            RaycastHit2D rayCastHit = 
            Physics2D.BoxCast(
                polygonCollider2D.bounds.center,
                size,
                0f,
                castDirection,
                offset,
                layerMask
            );      

            return rayCastHit.collider != null ? rayCastHit.collider.transform.tag : defaultResult;
        }

        public static GameObject DetectObject(PolygonCollider2D polygonCollider2D, LayerMask layerMask, Vector3 size, float offset) {
            RaycastHit2D rayCastHit = 
            Physics2D.BoxCast(
                polygonCollider2D.bounds.center,
                size,
                0f,
                Vector2.down,
                offset,
                layerMask
            );      

            return rayCastHit.collider != null ? rayCastHit.collider.gameObject : null;
        }

        public static List<string> DetectObjects(PolygonCollider2D polygonCollider2D, LayerMask layerMask, float offset) {
            List<string> detectedObjects = new List<string>();

            RaycastHit2D[] rayCastHits = 
            Physics2D.BoxCastAll(
                polygonCollider2D.bounds.center,
                polygonCollider2D.bounds.size,
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

        public static string DetectObject(PolygonCollider2D polygonCollider2D, LayerMask layerMask, Direction direction, float offset = 0.5f, string defaultResult = null, bool debug = false) {
            float xDirection = direction == Direction.RIGHT ? 1f : (direction == Direction.LEFT ? -1f : 0f);
            float yDirection = direction == Direction.UP ? 1f : (direction == Direction.DOWN ? -1f : 0f);

            Vector2 castDirection = new Vector2(xDirection, yDirection);

            RaycastHit2D rayCastHit = 
            Physics2D.Raycast (
                polygonCollider2D.bounds.center,
                castDirection,
                polygonCollider2D.bounds.size.x + offset,
                layerMask
            );

            Color rayColor = rayCastHit.collider != null ? Color.red : Color.green;

            Debug.DrawRay(
                polygonCollider2D.bounds.center,
                castDirection,
                rayColor
            );    

            return rayCastHit.collider != null ? rayCastHit.collider.transform.tag : defaultResult;
        }  
    }
}
