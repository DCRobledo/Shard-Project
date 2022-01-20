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
        
        private RelativeJoint2D grabJoint;

        private void Awake() {
            grabJoint = this.GetComponent<RelativeJoint2D>();
            grabJoint.enabled = false;
        } 


        private void Update() {
            grabbableObjects = CheckGrabbableObjects();
        }


        public void ReCall()
        {
            Debug.Log(objectToReCall.transform.tag);
        }

        public void Grab() 
        {
            // Grab the nearest object if we are not grabbing anything else
            grabJoint.connectedBody = grabJoint.connectedBody == null ? grabbableObjects[0].GetComponent<Rigidbody2D>() : null;
            grabJoint.enabled = grabJoint.connectedBody != null;
        }

        private List<GameObject> CheckGrabbableObjects() 
        {
            // Clear past objects
            foreach (GameObject grabbableObject in grabbableObjects) {
                Color newColor = grabbableObject.GetComponent<SpriteRenderer>().color;
                newColor.r -= .2f; newColor.g -= .2f; newColor.b -= .2f;
                grabbableObject.GetComponent<SpriteRenderer>().color = newColor;
            }
                
            // Check all near objects
            Collider2D[] nearObjects = Physics2D.OverlapCircleAll(this.gameObject.transform.position, grabRange);
            
            // Filter only grabables
            List<GameObject> detectedGrabbableObjects = new List<GameObject>();

            foreach (Collider2D collider in nearObjects)
                if (!detectedGrabbableObjects.Contains(collider.gameObject) && whatIsGrabbable.Contains(collider.gameObject.transform.tag))
                    detectedGrabbableObjects.Add(collider.gameObject);

            // Add visual cue for grabbable objects
            foreach (GameObject grabbableObject in detectedGrabbableObjects) {
                Color newColor = grabbableObject.GetComponent<SpriteRenderer>().color;
                newColor.r += .2f; newColor.g += .2f; newColor.b += .2f;
                grabbableObject.GetComponent<SpriteRenderer>().color = newColor;
            }

            return detectedGrabbableObjects;        
        }


        public void ChangeObjectToReCall(GameObject objectToReCall) { this.objectToReCall = objectToReCall; }
    }

}

