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
        private bool shouldCheckForRelease = false;

        [SerializeField]
        private Sprite releasedSprite;
        [SerializeField]
        private Sprite pressedSprite;
        

        private void OnTriggerEnter2D(Collider2D other) {
            if(canBePressedBy.Contains(other.tag)) {
                buttonEvent?.Invoke();

                this.GetComponent<SpriteRenderer>().sprite = pressedSprite;

                pressingEntity = other.gameObject;
                shouldCheckForRelease = true;
            }
        }

        private void Update() {
            if(shouldCheckForRelease)
                CheckForRelease();
        }


        private void CheckForRelease() {
            string detectedEntity = 
                Detection.DetectObject(
                    GetComponent<BoxCollider2D>(),
                     LayerMask.GetMask(LayerMask.LayerToName(pressingEntity.layer)),
                     Detection.Direction.UP,
                     GetComponent<BoxCollider2D>().bounds.size,
                     0.2f
                );
            
            if(pressingEntity.tag != detectedEntity) {
                if(buttonType == MechanismEnum.ButtonType.PRESS_RELEASE)
                    buttonEvent?.Invoke();

                this.GetComponent<SpriteRenderer>().sprite = releasedSprite;

                pressingEntity = null;
                shouldCheckForRelease = false;
            }
        }
    }
}


