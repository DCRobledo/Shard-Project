using Shard.Enums;
using Shard.Controllers;
using Shard.Lib.Custom;
using System;
using System.Collections;
using UnityEngine;

namespace Shard.Entities
{
    public class PlayerSFX : MonoBehaviour
    {
        [SerializeField]
        private EntityEnum.Entity entityType;
        public string entityNameNormalized;

        private EntityMovement movement;
        private EntityActions actions;

        private Action jumpSFX;
        private Action landSFX;


        private void Awake() {
            this.entityNameNormalized = entityType.ToString().Substring(0, 1) + (entityType.ToString().Substring(1)).ToLower();

            this.movement = this.GetComponent<EntityMovement>() as PlayerMovement;
            this.actions = this.GetComponent<EntityActions>() as PlayerActions;
        }

        private void OnEnable() {
            // Jump SFX
            jumpSFX += PlayJumpSFX;
            movement.SubscribeToJumpTrigger(jumpSFX);

            // Land SFX
            // landSFX += PlayLandSFX;
            // movement.SubscribeToLandTrigger(landSFX);

            // Grab SFX
            if(entityType == EntityEnum.Entity.PLAYER) {
                PlayerActions.grabTrigger += PlayGrabSFX;
                PlayerActions.dropTrigger += PlayGrabSFX;
            }

            // Walk SFX
            if(entityType == EntityEnum.Entity.PLAYER) {
                PlayerController.moveTrigger += PlayMoveSFX;
                PlayerController.stopTrigger += StopMoveSFX;
            }
        }

        private void OnDisable() {
            // Jump SFX
            jumpSFX -= PlayJumpSFX;
            movement.UnsubscribeFromJumpTrigger(jumpSFX);

            // // Land SFX
            // landSFX -= PlayLandSFX;
            // movement.UnsubscribeFromLandTrigger(landSFX);

            // Grab & Drop SFX
            if(entityType == EntityEnum.Entity.PLAYER) {
                PlayerActions.grabTrigger -= PlayGrabSFX;
                PlayerActions.dropTrigger -= PlayGrabSFX;
            }

            // Walk SFX
            if(entityType == EntityEnum.Entity.PLAYER) {
                PlayerController.moveTrigger -= PlayMoveSFX;
                PlayerController.stopTrigger -= StopMoveSFX;
            }
        }

        private void PlayJumpSFX() { AudioController.Instance.Play(entityNameNormalized + "Jump"); }
        private void PlayLandSFX() { AudioController.Instance.Play(entityNameNormalized + "Land"); }
        private void PlayGrabSFX() { AudioController.Instance.Play(entityNameNormalized + "Grab"); }
        private void PlayDropSFX() { AudioController.Instance.Play(entityNameNormalized + "Drop"); }

        private void PlayMoveSFX() { AudioController.Instance.Play(entityNameNormalized + "Move"); }
        private void StopMoveSFX() { AudioController.Instance.Stop(entityNameNormalized + "Move"); }
    }
}


