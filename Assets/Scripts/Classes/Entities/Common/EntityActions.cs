using Shard.Gameflow;
using Shard.Lib.Custom;
using Shard.Enums;
using Shard.Controllers;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shard.Entities
{
    public abstract class EntityActions : MonoBehaviour
    {
        #if UNITY_EDITOR 
            [TagSelector]
         #endif
        [SerializeField]
        protected string[] whatIsGrabbable = new string[] { };

        [SerializeField]
        protected float grabRange = 1f;

        protected List<GameObject> grabbableObjects = new List<GameObject>();
        
        protected RelativeJoint2D grabJoint;

        [SerializeField]
        protected UnityEvent grabTrigger;


        protected virtual void Awake() {
            grabJoint = this.GetComponent<RelativeJoint2D>();
            grabJoint.enabled = false;
        } 

        private void OnEnable() {
            // Subscribe the death event
            CheckPointsManagement.playerDeathEvent += Grab;
        }

        private void OnDisable() {
            // Unsubscribe the death event
            CheckPointsManagement.playerDeathEvent += Grab;
        }

        private void Update() {
            grabbableObjects = CheckGrabbableObjects();
        }
        

        public virtual void ReCall() {}

        public virtual void ChangeObjectToReCall(GameObject objectToReCall) {}


        public void ExecuteAction(EntityEnum.Action action) {
            switch (action)
            {
                case EntityEnum.Action.RECALL:     ReCall(); break;
                case EntityEnum.Action.GRAB:       Grab();   break;
                case EntityEnum.Action.RELEASE:    Grab();   break;
                case EntityEnum.Action.THROW:                break;
                case EntityEnum.Action.CLAP:                 break;
            }
        }

        public void Grab() 
        {
            // Grab the nearest object if we are not grabbing anything else
            if(grabbableObjects.Count > 0)
            {
                // Tell the robot that it is being grabbed
                if(grabbableObjects[0]?.tag == "Robot")
                    RobotController.Instance.ToggleIsRobotGrabbed();

                grabJoint.connectedBody = grabJoint.connectedBody == null ? grabbableObjects[0].GetComponent<Rigidbody2D>() : null;
                grabJoint.enabled = grabJoint.connectedBody != null;

                grabTrigger?.Invoke();
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

