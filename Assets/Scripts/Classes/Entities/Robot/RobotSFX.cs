using Shard.Enums;
using Shard.Controllers;
using Shard.Lib.Custom;
using System;
using System.Collections;
using UnityEngine;

namespace Shard.Entities
{
    public class RobotSFX : EntitySFX
    {
        private new void Awake() {
            this.movement = this.GetComponent<EntityMovement>() as RobotMovement;
            this.actions = this.GetComponent<EntityActions>() as RobotActions;

            base.Awake();
        }

        private new void OnEnable() {
            // Walk SFX
            RobotMovement.moveTrigger += PlayMoveSFX;
            RobotMovement.stopTrigger += StopMoveSFX;

            // Turn_On SFX
            RobotController.turnOnTrigger += PlayTurnOnSFX;

            // Turn_Off SFX
            RobotController.turnOffTrigger += PlayTurnOffSFX;

            base.OnEnable();
        }

        private new void OnDisable() {
            // Walk SFX
            RobotMovement.moveTrigger += PlayMoveSFX;
            RobotMovement.stopTrigger += StopMoveSFX;

            // Turn_On SFX
            RobotController.turnOnTrigger -= PlayTurnOnSFX;

            // Turn_Off SFX
            RobotController.turnOffTrigger -= PlayTurnOffSFX;

            base.OnDisable();
        }

        protected void PlayTurnOnSFX() { AudioController.Instance.Play("RobotTurnOn"); }
        protected void PlayTurnOffSFX() { AudioController.Instance.Play("RobotTurnOff"); }
    }
}


