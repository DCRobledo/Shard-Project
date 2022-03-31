using Shard.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Mechanisms
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private MechanismEnum.DoorState initialState;

        [SerializeField]
        private float movingSpeed = 0.1f;
        [SerializeField]
        private float movingDistance = 2f;

        private Vector3 originalPosition;

        private bool isOpen;

        [SerializeField]
        private List<GameObject> openingButtons;


        private void Awake() {
            isOpen = initialState == MechanismEnum.DoorState.OPEN;

            originalPosition = this.transform.localPosition; 
        }

        private void OnEnable() {
            foreach (GameObject button in openingButtons)
                button.GetComponent<Button>().buttonPressEvent += Open;
        }

        private void OnDisable() {
            foreach (GameObject button in openingButtons)
                button.GetComponent<Button>().buttonPressEvent -= Open;
        }


        public void Open() { if(!isOpen) StartCoroutine(OpenAnimation()); }

        private IEnumerator OpenAnimation() {
            Vector3 doorPosition = this.transform.localPosition;

            do {
                doorPosition.y += movingSpeed;
                this.transform.localPosition = doorPosition;

                yield return null;
            } while (doorPosition.y < originalPosition.y + movingDistance);

            isOpen = true;
        }

        public void Close() { if(isOpen) StartCoroutine(CloseAnimation()); }

        private IEnumerator CloseAnimation() {
            Vector3 doorPosition = this.transform.localPosition;

            do {
                doorPosition.y -= movingSpeed;
                this.transform.localPosition = doorPosition;

                yield return null;
            } while (doorPosition.y > originalPosition.y);

            isOpen = false;
        }
    }
}

