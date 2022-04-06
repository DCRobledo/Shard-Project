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

        [SerializeField] [Range (1, 10)]
        private int movingSpeed;
        [SerializeField]
        private float movingDistance = 2f;

        private bool isOpen;

        [SerializeField]
        private List<GameObject> openingButtons;
        [SerializeField]
        private List<GameObject> closingButtons;
        [SerializeField]
        private List<GameObject> toggleButtons;


        private void Awake() {
            isOpen = initialState == MechanismEnum.DoorState.OPEN;
        }

        private void OnEnable() {
            foreach (GameObject button in openingButtons)
                button.GetComponent<Button>().buttonEvent += Open;

            foreach (GameObject button in closingButtons)
                button.GetComponent<Button>().buttonEvent += Close;

            foreach (GameObject button in toggleButtons)
                button.GetComponent<Button>().buttonEvent += Toggle;
        }

        private void OnDisable() {
            foreach (GameObject button in openingButtons)
                button.GetComponent<Button>().buttonEvent -= Open;

            foreach (GameObject button in closingButtons)
                button.GetComponent<Button>().buttonEvent -= Close;
            
            foreach (GameObject button in toggleButtons)
                button.GetComponent<Button>().buttonEvent += Toggle;
        }


        public void Open() { if(!isOpen) StartCoroutine(OpenAnimation()); }

        private IEnumerator OpenAnimation() {
            Vector3 startingPosition = this.transform.localPosition;
            Vector3 doorPosition = startingPosition;

            do {
                doorPosition.y += movingSpeed * 0.01f;
                this.transform.localPosition = doorPosition;

                yield return null;
            } while (doorPosition.y < startingPosition.y + movingDistance);

            isOpen = true;
        }

        public void Close() { if(isOpen) StartCoroutine(CloseAnimation()); }

        private IEnumerator CloseAnimation() {
            Vector3 startingPosition = this.transform.localPosition;
            Vector3 doorPosition = startingPosition;

            do {
                doorPosition.y -= movingSpeed * 0.01f;
                this.transform.localPosition = doorPosition;

                yield return null;
            } while (doorPosition.y > startingPosition.y - movingDistance);

            isOpen = false;
        }
    
        public void Toggle() { if(!isOpen) StartCoroutine(OpenAnimation()); else StartCoroutine(CloseAnimation()); }
    }
}

