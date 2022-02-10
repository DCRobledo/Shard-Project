using Shard.Controllers;
using Shard.Lib.Custom;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Entities

{
    public class EntityActions : MonoBehaviour
    {
        public enum Action {
            RECALL,
            GRAB,
            RELEASE,
            THROW,
            CLAP,
            USE_OBJECT
        }

        #if UNITY_EDITOR 
            [TagSelector]
         #endif
        [SerializeField]
        private string[] whatIsGrabbable = new string[] { };

        [SerializeField]
        private float grabRange = 1f;
        [SerializeField]
        private float reCallCoolDown = 1f;

        [SerializeField]
        private GameObject objectToReCall;

        private List<GameObject> grabbableObjects = new List<GameObject>();
        
        private RelativeJoint2D grabJoint;

        private bool canReCall = true;


        private void Awake() {
            objectToReCall = GameObject.Find("robot");

            grabJoint = this.GetComponent<RelativeJoint2D>();
            grabJoint.enabled = false;
        } 

        private void Update() {
            grabbableObjects = CheckGrabbableObjects();
        }

        public void ChangeObjectToReCall(GameObject objectToReCall) { this.objectToReCall = objectToReCall; }


        public void ExecuteAction(Action action) {
            switch (action)
            {
                case Action.RECALL:     ReCall(); break;
                case Action.GRAB:       Grab();   break;
                case Action.RELEASE:    Grab();   break;
                case Action.THROW:                break;
                case Action.CLAP:                 break;
                case Action.USE_OBJECT:           break;
            }
        }


        public void ReCall()
        {
            if (canReCall) {
                // Recall the entity
                Vector3 entityPosition = this.GetComponent<Transform>().position;
                objectToReCall.GetComponent<Transform>().position = new Vector3(entityPosition.x + 1.5f, entityPosition.y + 1f);

                // Start cooldown
                StartCoroutine(ReCallCoolDown(reCallCoolDown));
            }
        }

        private IEnumerator ReCallCoolDown(float seconds) {
            canReCall = false;

            yield return new WaitForSeconds(seconds);

            canReCall = true;
        }

        public void Grab() 
        {
            // Grab the nearest object if we are not grabbing anything else
            if(grabbableObjects.Count > 0)
            {
                grabJoint.connectedBody = grabJoint.connectedBody == null ? grabbableObjects[0].GetComponent<Rigidbody2D>() : null;
                grabJoint.enabled = grabJoint.connectedBody != null;
            }
        }

        private List<GameObject> CheckGrabbableObjects() 
        {
            // Clear past objects
            VisualUtils.ChangeObjectsColor(ref grabbableObjects, -.2f, -.2f, -.2f);
                
            // Detect grabbable objects
            List<GameObject> detectedGrabbableObjects = Detection.DetectNearObjects(this.gameObject.transform.position, grabRange, whatIsGrabbable);

            // Add visual cue for grabbable objects
            VisualUtils.ChangeObjectsColor(ref detectedGrabbableObjects, .2f, .2f, .2f);

            return detectedGrabbableObjects;        
        }
    }
}

