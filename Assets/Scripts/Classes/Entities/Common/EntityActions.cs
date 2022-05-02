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
            CheckPointsManagement.playerDeathEvent -= Grab;
        }

        private void Update() {
            grabbableObjects = CheckGrabbableObjects();
        }
        

        public virtual void ReCall() {}

        public virtual void ChangeObjectToReCall(GameObject objectToReCall) {}


        public void ExecuteAction(EntityEnum.Action action) {
            switch (action)
            {
                case EntityEnum.Action.RECALL: ReCall(); break;
                case EntityEnum.Action.GRAB:   Grab();   break;
                case EntityEnum.Action.DROP:   Grab();   break;
            }
        }

        public virtual void Grab() 
        {
            // If we are grabbing, we release the object
            if(grabJoint.connectedBody != null) {
                // Tell the robot that it is no longer being grabbed
                if(grabJoint.connectedBody.tag == "Robot")
                    RobotController.Instance.ToggleIsRobotGrabbed();

                grabJoint.connectedBody = null;
                grabJoint.enabled = false;
            }

            else {
                // Grab the nearest object if we are not grabbing anything else
                if(grabbableObjects.Count > 0)
                {
                    // Tell the robot that it is being grabbed
                    if(grabbableObjects[0]?.tag == "Robot")
                        RobotController.Instance.ToggleIsRobotGrabbed();

                    grabJoint.connectedBody = grabbableObjects[0].GetComponent<Rigidbody2D>();
                    grabJoint.enabled = true;
                }
            }
        }

        private List<GameObject> CheckGrabbableObjects() 
        {
            return Detection.DetectNearObjects(this.gameObject.transform.position, grabRange, whatIsGrabbable);        
        }
    }
}

