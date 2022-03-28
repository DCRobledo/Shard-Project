using System;
using System.Collections;
using UnityEngine;

namespace Shard.Entities
{
    public class PlayerActions : EntityActions
    {
        [SerializeField]
        private float reCallCoolDown = 1f;

        [SerializeField]
        private GameObject objectToReCall;

        private bool canReCall = true;

        public static Action grabTrigger;
        public static Action dropTrigger;


        protected override void Awake() {
            objectToReCall = GameObject.Find("robot");

            base.Awake();
        }


        public override void ChangeObjectToReCall(GameObject objectToReCall) { this.objectToReCall = objectToReCall; }
    
        public override void ReCall()
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
    
        public override void Grab() {
             if(grabJoint.connectedBody != null) dropTrigger?.Invoke();
             else if(grabbableObjects.Count > 0) grabTrigger?.Invoke();

             base.Grab();
        }
    }
}
