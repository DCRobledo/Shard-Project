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

        protected void PlayJumpSFX() { AudioController.Instance.Play(entityNameNormalized + "Jump"); }
        protected void PlayLandSFX() { AudioController.Instance.Play(entityNameNormalized + "Land"); }
        protected void PlayGrabSFX() { AudioController.Instance.Play(entityNameNormalized + "Grab"); }
        protected void PlayDropSFX() { AudioController.Instance.Play(entityNameNormalized + "Drop"); }

        protected void PlayMoveSFX() { AudioController.Instance.Play(entityNameNormalized + "Move"); }
        protected void StopMoveSFX() { AudioController.Instance.Stop(entityNameNormalized + "Move"); }
    }
}


