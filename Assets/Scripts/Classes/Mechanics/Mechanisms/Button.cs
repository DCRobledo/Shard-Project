using Shard.Enums;
using Shard.Lib.Custom;
using System;
using System.Collections;
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

        private bool shouldCheckForPress = true;
        private bool shouldCheckForRelease = false;

        private int numberOfPressingEntites;

        [SerializeField]
        private float pressDelay;

        [SerializeField]
        private Sprite releasedSprite;
        [SerializeField]
        private Sprite pressedSprite;
        

        private void OnTriggerEnter2D(Collider2D other) {
            if(shouldCheckForPress && canBePressedBy.Contains(other.tag)) {
                Debug.Log("Pressed");
                Press();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(shouldCheckForRelease && canBePressedBy.Contains(other.tag)) {
                Debug.Log("Released");
                Release();
            }
        }

        private void Press() {
            buttonEvent?.Invoke(this.gameObject, true);

            AudioController.Instance.Play("ButtonPressed");

            this.GetComponent<SpriteRenderer>().sprite = pressedSprite;

            shouldCheckForRelease = true;
            shouldCheckForPress = false;
        }

        private void Release() {
            if(buttonType == MechanismEnum.ButtonType.PRESS_RELEASE)
                buttonEvent?.Invoke(this.gameObject, false);
            
            AudioController.Instance.Play("ButtonReleased");
                
            this.GetComponent<SpriteRenderer>().sprite = releasedSprite;

            shouldCheckForRelease = false;
            StartCoroutine(PressDelay());
        }

        private IEnumerator PressDelay() {
            shouldCheckForPress = false;

            yield return new WaitForSeconds(pressDelay);

            shouldCheckForPress = true;
        }
    }
}


