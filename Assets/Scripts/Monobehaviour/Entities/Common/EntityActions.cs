using Shard.Utils;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities

{
    public class EntityActions : MonoBehaviour
    {
        [TagSelector]
        public string[] whatIsGrabbable = new string[] { };

        [SerializeField]
        private float grabRange = 1f;

        [SerializeField]
        private GameObject objectToReCall;

        private List<GameObject> grabbableObjects = new List<GameObject>();


        private void Update() {
            grabbableObjects = CheckGrabbableObjects();
        }


        public void ReCall()
        {
            Debug.Log(objectToReCall.transform.tag);
        }

        public void Grab() 
        {
            // Grab the nearest grabbable object
            Destroy(grabbableObjects[0]);

        }

        private List<GameObject> CheckGrabbableObjects() 
        {
            // Check all near objects
            Collider2D[] nearObjects = Physics2D.OverlapCircleAll(this.gameObject.transform.position, grabRange);
            
            // Filter only grabables
            List<GameObject> detectedGrabbableObjects = new List<GameObject>();

            foreach (Collider2D collider in nearObjects)
                if (!detectedGrabbableObjects.Contains(collider.gameObject) && whatIsGrabbable.Contains(collider.gameObject.transform.tag))
                    detectedGrabbableObjects.Add(collider.gameObject);

            // // Add visual cue for grabbable objects
            // foreach (GameObject grabbableObject in detectedGrabbableObjects) {
            //     Color newColor = grabbableObject.GetComponent<SpriteRenderer>().color;
            //     newColor.r += 10f; newColor.g += 10f; newColor.b += 10f;
            //     grabbableObject.GetComponent<SpriteRenderer>().color = newColor;
            // }

            return detectedGrabbableObjects;        
        }


        public void ChangeObjectToReCall(GameObject objectToReCall) { this.objectToReCall = objectToReCall; }
    }

}

