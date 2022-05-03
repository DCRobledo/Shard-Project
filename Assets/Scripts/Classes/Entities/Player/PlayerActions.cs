using Shard.Controllers;
using Shard.Lib.Custom;
using Shard.Gameflow;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Shard.Entities
{
    public class PlayerActions : EntityActions
    {
        [SerializeField]
        private float reCallCoolDown = 1f;

        private bool isReCallInCooldown = false;

        public static Action grabTrigger;
        public static Action dropTrigger;

        [SerializeField]
        private UnityEvent robotGrabbedEvent;
        [SerializeField]
        private UnityEvent robotDroppedEvent;

    
        public override void ReCall()
        {
            if (!isReCallInCooldown) {
                // Check if we are inside a recall zone
                GameObject reCallZone = Detection.DetectObject(
                    this.GetComponent<PolygonCollider2D>(),
                    LayerMask.GetMask("ReCallZones"),
                    this.GetComponent<PolygonCollider2D>().bounds.size,
                    0.2f
                );
                
                if (reCallZone != null)
                {
                    // Recall the robot
                    StartCoroutine(reCallZone.GetComponent<ReCallZone>().ReCallRobot());

                    // Play recall SFX 
                    AudioController.Instance.Play("ReCall");

                    // Start cooldown
                    StartCoroutine(ReCallCoolDown(reCallCoolDown));
                }
            }
        }

        private IEnumerator ReCallCoolDown(float seconds) {
            isReCallInCooldown = true;

            yield return new WaitForSeconds(seconds);

            isReCallInCooldown = false;
        }

    
        public override void Grab() {
            // If we are grabbing something, we release it
            if(grabJoint.connectedBody != null) {
                dropTrigger?.Invoke();

                if(grabJoint.connectedBody.tag == "Robot") {
                    PlayerController.Instance.ExpandPlayerCollider(false);
                    robotDroppedEvent?.Invoke();
                }
                    
            }

            base.Grab();

            // If code reaches here and we are still grabbing, it means we grabbed instead of dropping
            if(grabJoint.connectedBody != null) {
                grabTrigger?.Invoke();

                if(grabJoint.connectedBody.tag == "Robot") {
                    PlayerController.Instance.ExpandPlayerCollider(true);
                    robotGrabbedEvent?.Invoke();
                }
            }
        }
    }
}
