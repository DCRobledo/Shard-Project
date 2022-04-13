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

        public Action<GameObject, bool> buttonEvent;

        private GameObject pressingEntity;
        private bool shouldCheckForRelease = false;

        [SerializeField]
        private Sprite releasedSprite;
        [SerializeField]
        private Sprite pressedSprite;
        

        private void OnCollisionEnter2D(Collision2D other) {
            if(canBePressedBy.Contains(other.collider.tag)) {
                buttonEvent?.Invoke(this.gameObject, true);

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
                    GetComponent<PolygonCollider2D>(),
                     LayerMask.GetMask(LayerMask.LayerToName(pressingEntity.layer)),
                     Detection.Direction.UP,
                     GetComponent<PolygonCollider2D>().bounds.size,
                     0.2f
                );
            
            if(pressingEntity.tag != detectedEntity) {
                if(buttonType == MechanismEnum.ButtonType.PRESS_RELEASE)
                    buttonEvent?.Invoke(this.gameObject, false);

                this.GetComponent<SpriteRenderer>().sprite = releasedSprite;

                pressingEntity = null;
                shouldCheckForRelease = false;
            }
        }
    }
}


