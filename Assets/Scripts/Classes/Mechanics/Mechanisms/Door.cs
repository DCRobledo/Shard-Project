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
        private bool isClosing;

        [SerializeField]
        private List<GameObject> openingButtons;
        [SerializeField]
        private List<(GameObject, bool)> requiredButtonsToOpen;

        [SerializeField]
        private List<GameObject> closingButtons;
        [SerializeField]
        private List<GameObject> toggleButtons;

        [SerializeField]
        private List<ButtonConnection> buttonConnections;


        private void Awake() {
            isOpen = initialState == MechanismEnum.DoorState.OPEN;
        }

        private void OnEnable() {
            // foreach (GameObject button in openingButtons)
            //     button.GetComponent<Button>().buttonEvent += Open;

            // foreach (GameObject button in closingButtons)
            //     button.GetComponent<Button>().buttonEvent += Close;

            // foreach (GameObject button in toggleButtons)
            //     button.GetComponent<Button>().buttonEvent += Toggle;

            for(int i = 0; i < buttonConnections.Count; i++)
                for(int j = 0; j < buttonConnections[i].GetButtons().Count; j++)
                    buttonConnections[i].GetButtons()[j].GetButton().GetComponent<Button>().buttonEvent += UpdateButtonState;
        }

        private void OnDisable() {
            // foreach (GameObject button in openingButtons)
            //     button.GetComponent<Button>().buttonEvent -= Open;

            // foreach (GameObject button in closingButtons)
            //     button.GetComponent<Button>().buttonEvent -= Close;
            
            // foreach (GameObject button in toggleButtons)
            //     button.GetComponent<Button>().buttonEvent += Toggle;

            for(int i = 0; i < buttonConnections.Count; i++)
                for(int j = 0; j < buttonConnections[i].GetButtons().Count; j++)
                    buttonConnections[i].GetButtons()[j].GetButton().GetComponent<Button>().buttonEvent -= UpdateButtonState;
        }


        public void Open() {
            if(!isOpen && !isClosing)
                StartCoroutine(OpenAnimation());
        }

        private IEnumerator OpenAnimation() {
            isClosing = false;

            Vector3 startingPosition = this.transform.localPosition;
            Vector3 doorPosition = startingPosition;

            do {
                doorPosition.y += movingSpeed * 0.01f;
                this.transform.localPosition = doorPosition;

                yield return null;
            } while (doorPosition.y < startingPosition.y + movingDistance);

            isOpen = true;
        }

        public void Close() {
            if(isOpen)
                StartCoroutine(CloseAnimation());
        }

        private IEnumerator CloseAnimation() {
            isClosing = true;

            Vector3 startingPosition = this.transform.localPosition;
            Vector3 doorPosition = startingPosition;

            do {
                doorPosition.y -= movingSpeed * 0.01f;
                this.transform.localPosition = doorPosition;

                yield return null;
            } while (doorPosition.y > startingPosition.y - movingDistance);

            isOpen = false;
        }
    
        public void Toggle() {
            if(!isOpen)
                StartCoroutine(OpenAnimation());
            else
                StartCoroutine(CloseAnimation());
        }
    

        private void UpdateButtonState(GameObject button, bool state) {
            foreach (ButtonConnection buttonConnection in buttonConnections)
                buttonConnection.UpdateButtonState(button, state);
        }
    }

    [System.Serializable]
    public class ButtonConnection {
        [SerializeField]
        private List<ButtonTracker> buttons;
        [SerializeField]
        private MechanismEnum.DoorAction action;

        public ButtonConnection(List<ButtonTracker> buttons, MechanismEnum.DoorAction action)
        {
            this.buttons = buttons;
            this.action = action;
        }

        public void UpdateButtonState(GameObject button, bool state) {
            foreach (ButtonTracker myButton in buttons)
                if(myButton.GetButton() == button)
                    myButton.SetState(state);
        }

        public bool IsActive() {
            foreach (ButtonTracker button in buttons)
                if(!button.IsActive()) return false;
            
            return true;
        }

        public List<ButtonTracker> GetButtons() { return this.buttons; }
    }

    [System.Serializable]
    public class ButtonTracker {
        [SerializeField]
        private GameObject button;
        [SerializeField]
        private bool state;

        public ButtonTracker(GameObject button, bool state)
        {
            this.button = button;
            this.state = state;
        }

        public bool IsActive() { return this.state; }
        public void SetState(bool state) { this.state = state; }

        public GameObject GetButton() { return this.button; }
    }
}

