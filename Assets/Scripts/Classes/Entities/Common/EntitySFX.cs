using Shard.Enums;
using Shard.Controllers;
using Shard.Lib.Custom;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        protected Coroutine moveSFXProtection = null;

        protected Vector2 lastPosition;


        protected void Awake() {
            // Transform the enum format (PLAYER) to the normalized format (Player)
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

        

        private IEnumerator MoveSFXProtection() {
            // Check if the entity is walking towards a wall
            while(true) {
                Vector2 lastPosition = this.transform.localPosition;
            
                yield return new WaitForSeconds(0.1f);
                
                Vector2 currentPosition = this.transform.localPosition;

                if(currentPosition.x == lastPosition.x) {
                    isStuckInWall = true;
                    StopMoveSFX();
                }

                else
                    isStuckInWall = false;
            }
        }

        protected void PlayJumpSFX() { AudioController.Instance.Play(entityNameNormalized + "Jump"); }
        
        protected void PlayLandSFX() { AudioController.Instance.Play(entityNameNormalized + "Land"); }
        
        protected void PlayGrabSFX() { AudioController.Instance.Play(entityNameNormalized + "Grab"); }
        
        protected void PlayDropSFX() { AudioController.Instance.Play(entityNameNormalized + "Drop"); }

        protected void PlayMoveSFX() {
            if(!isStuckInWall) {
                AudioController.Instance.Play(entityNameNormalized + "Move");

                // Check if we need to stop the move SFX
                if(moveSFXProtection == null) 
                    moveSFXProtection = StartCoroutine(MoveSFXProtection());
            } 
        }
        
        protected void StopMoveSFX() { 
            AudioController.Instance.Stop(entityNameNormalized + "Move");
        }
    }
}


