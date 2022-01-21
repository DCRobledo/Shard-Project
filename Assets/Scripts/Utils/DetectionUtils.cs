using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Utils
{
    public static class DetectionUtils
    {
        public static List<GameObject> DetectNearObjects(Vector3 center, float radius) {
            List<GameObject> detectedGameObjects = new List<GameObject>();

            // Check all near 
            Collider2D[] nearObjects = Physics2D.OverlapCircleAll(center, radius);
            
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
    
        public static bool DetectGround(BoxCollider2D boxCollider2D, LayerMask whatIsGround, bool debug = false) {
            bool isGrounded;

            // Check ground through raycasting the circle collider
            RaycastHit2D rayCastHit = 
            Physics2D.BoxCast(
                boxCollider2D.bounds.center,
                boxCollider2D.bounds.size,
                0f,
                Vector2.down,
                1f,
                whatIsGround
            );      

            isGrounded = rayCastHit.collider != null;

            if (debug) {
                // Debug the raycast performed in overlapcircle
                Color rayColor = isGrounded ? Color.green : Color.red;
                DebugUtils.DebugBoxRayCast(
                    boxCollider2D.bounds.center,
                    boxCollider2D.bounds.extents.x,
                    boxCollider2D.bounds.extents.y + .25f,
                    rayColor
                );
            }
            

            return isGrounded;
        } 
    }
}
