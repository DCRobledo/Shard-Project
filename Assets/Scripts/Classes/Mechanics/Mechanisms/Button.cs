using Shard.Enums;
using Shard.Lib.Custom;
using System;
using System.Collections;
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

        private bool shouldCheckForPress = true;
        private bool shouldCheckForRelease = false;

        [SerializeField]
        private float pressDelay;

        [SerializeField]
        private Sprite releasedSprite;
        [SerializeField]
        private Sprite pressedSprite;
        

        private void OnTriggerEnter2D(Collider2D other) {
            if(shouldCheckForPress && canBePressedBy.Contains(other.tag)) {
                buttonEvent?.Invoke(this.gameObject, true);

                this.GetComponent<SpriteRenderer>().sprite = pressedSprite;

                shouldCheckForRelease = true;

                StartCoroutine(PressDelay());
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
                     LayerMask.GetMask(canBePressedBy),
                     Detection.Direction.UP,
                     GetComponent<PolygonCollider2D>().bounds.size,
                     0.2f
                );
            
            if(detectedEntity == null) {
                if(buttonType == MechanismEnum.ButtonType.PRESS_RELEASE)
                    buttonEvent?.Invoke(this.gameObject, false);

                this.GetComponent<SpriteRenderer>().sprite = releasedSprite;

                shouldCheckForRelease = false;
            }
        }

        private IEnumerator PressDelay() {
            shouldCheckForPress = false;

            yield return new WaitForSeconds(pressDelay);

            shouldCheckForPress = true;
        }
    }
}


