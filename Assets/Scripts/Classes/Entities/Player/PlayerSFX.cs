using Shard.Enums;
using Shard.Controllers;
using Shard.Lib.Custom;
using System;
using System.Collections;
using UnityEngine;

namespace Shard.Entities
{
    public class PlayerSFX : EntitySFX
    {
        private new void Awake() {
            this.movement = this.GetComponent<EntityMovement>() as PlayerMovement;
            this.actions = this.GetComponent<EntityActions>() as PlayerActions;

            base.Awake();
        }

        private new void OnEnable() {
            // Grab SFX
            PlayerActions.grabTrigger += PlayGrabSFX;
            PlayerActions.dropTrigger += PlayGrabSFX;

            // Walk SFX
            PlayerController.moveTrigger += PlayMoveSFX;
            PlayerController.stopTrigger += StopMoveSFX;

            base.OnEnable();
        }

        private new void OnDisable() {
            // Grab SFX
            PlayerActions.grabTrigger += PlayGrabSFX;
            PlayerActions.dropTrigger += PlayGrabSFX;

            // Walk SFX
            PlayerController.moveTrigger += PlayMoveSFX;
            PlayerController.stopTrigger += StopMoveSFX;

            base.OnDisable();
        }
    }
}


