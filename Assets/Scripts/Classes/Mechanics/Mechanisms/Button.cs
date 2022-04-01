using Shard.Enums;
using Shard.Lib.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shard.Mechanisms
{
    public class Button : MonoBehaviour
    {
        [SerializeField]
        private MechanismEnum.ButtonType buttonType;

        #if UNITY_EDITOR 
            [TagSelector]
         #endif
        [SerializeField]
        protected string[] canBePressedBy = new string[] { };

        public Action buttonEvent;

        private GameObject pressingEntity;
        public bool shouldCheckForRelease = false;


        private void OnTriggerEnter2D(Collider2D other) {
            if(canBePressedBy.Contains(other.tag)) {
                buttonEvent?.Invoke();

                if(buttonType == MechanismEnum.ButtonType.PRESS_RELEASE) {
                    pressingEntity = other.gameObject;
                    shouldCheckForRelease = true;
                }
            }
        }

        private void Update() {
            if(shouldCheckForRelease)
                CheckForRelease();
        }


        private void CheckForRelease() {
            string detectedEntity = Detection.DetectObject(GetComponent<BoxCollider2D>(), LayerMask.GetMask(LayerMask.LayerToName(pressingEntity.layer)), Detection.Direction.UP, 0.2f);
            if(pressingEntity.tag != detectedEntity) {
                buttonEvent?.Invoke();

                pressingEntity = null;
                shouldCheckForRelease = false;
            }
        }
    }
}


