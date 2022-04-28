using Shard.Enums;
using Shard.Controllers;
using Shard.Lib.Custom;
using System;
using System.Collections;
using UnityEngine;

namespace Shard.Entities
{
    public class EntitySFX : MonoBehaviour
    {
        [SerializeField]
        protected EntityEnum.Entity entityType;
        protected string entityNameNormalized;

        protected EntityMovement movement;
        protected EntityActions actions;

        protected Action jumpSFX;
        protected Action landSFX;

        protected bool isMoveSFXPlaying = false;
        protected bool isStuckInWall = false;

        private Coroutine moveSFXProtection;

        private Vector2 lastPosition;


        protected void Awake() {
            this.entityNameNormalized = entityType.ToString().Substring(0, 1) + (entityType.ToString().Substring(1)).ToLower();
        }

        protected void OnEnable() {
            // Jump SFX
            jumpSFX += PlayJumpSFX;
            movement.SubscribeToJumpTrigger(jumpSFX);

            // Land SFX
            // landSFX += PlayLandSFX;
            // movement.SubscribeToLandTrigger(landSFX);
        }

        protected void OnDisable() {
            // Jump SFX
            jumpSFX -= PlayJumpSFX;

            // // Land SFX
            // landSFX -= PlayLandSFX;
        }

        private void Update() {
            if((isMoveSFXPlaying || isStuckInWall) && moveSFXProtection == null) 
                moveSFXProtection = StartCoroutine(MoveSFXProtection());
        }

        private IEnumerator MoveSFXProtection() {
            Vector2 lastPosition = this.transform.localPosition;

            yield return new WaitForSeconds(0.1f);

            // Check if the entity is walking towards a wall
            Vector2 currentPosition = this.transform.localPosition;

            if (currentPosition.x == lastPosition.x) {
                isStuckInWall = true;

                StopMoveSFX();
            }
            else 
                isStuckInWall = false; 

            moveSFXProtection = null;
        }

        protected void PlayJumpSFX() { AudioController.Instance.Play(entityNameNormalized + "Jump"); }
        protected void PlayLandSFX() { AudioController.Instance.Play(entityNameNormalized + "Land"); }
        protected void PlayGrabSFX() { AudioController.Instance.Play(entityNameNormalized + "Grab"); }
        protected void PlayDropSFX() { AudioController.Instance.Play(entityNameNormalized + "Drop"); }

        protected void PlayMoveSFX() {
            if(!isStuckInWall) {
                AudioController.Instance.Play(entityNameNormalized + "Move");

                isMoveSFXPlaying = true;
            } 
        }
        protected void StopMoveSFX() { 
            AudioController.Instance.Stop(entityNameNormalized + "Move");

            isMoveSFXPlaying = false;
        }
    }
}


