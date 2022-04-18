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

        private Vector3 openPosition;
        private Vector3 closePosition;

        private Coroutine openAnimation;
        private Coroutine closeAnimation;

        [SerializeField]
        private List<ButtonConnection> buttonConnections;


        private void Awake() {
            if(initialState == MechanismEnum.DoorState.OPEN) {
                openPosition = this.transform.localPosition;
                closePosition = openPosition - new Vector3(0, movingDistance, 0);
            } else {
                closePosition = this.transform.localPosition;
                openPosition = closePosition + new Vector3(0, movingDistance, 0);
            }
        }

        private void OnEnable() {
            for(int i = 0; i < buttonConnections.Count; i++)
                for(int j = 0; j < buttonConnections[i].GetButtons().Count; j++)
                    buttonConnections[i].GetButtons()[j].GetButton().GetComponent<Button>().buttonEvent += UpdateButtonState;
        }

        private void OnDisable() {
            for(int i = 0; i < buttonConnections.Count; i++)
                for(int j = 0; j < buttonConnections[i].GetButtons().Count; j++)
                    buttonConnections[i].GetButtons()[j].GetButton().GetComponent<Button>().buttonEvent -= UpdateButtonState;
        }

        private void Open() {
            if(closeAnimation != null) {
                StopCoroutine(closeAnimation);
                closeAnimation = null;
            } 

            if(openAnimation == null)
                openAnimation = StartCoroutine(OpenAnimation());
        }

        private IEnumerator OpenAnimation() {
            Vector3 doorPosition = this.transform.localPosition;

            while (doorPosition.y < openPosition.y) {
                doorPosition.y += movingSpeed * 0.01f;
                this.transform.localPosition = doorPosition;

                yield return null;
            }
        }

        private void Close() {
            if(openAnimation != null) {
                StopCoroutine(openAnimation);
                openAnimation = null;
            } 

            if(closeAnimation == null)
                closeAnimation = StartCoroutine(CloseAnimation());
        }

        private IEnumerator CloseAnimation() {
            Vector3 doorPosition = this.transform.localPosition;
            
            while (doorPosition.y > closePosition.y) {
                doorPosition.y -= movingSpeed * 0.01f;
                this.transform.localPosition = doorPosition;

                yield return null;
            } 
        }
    
        public void Toggle() {
            // If both animations are null, then it is the first time the door is activated
            if (openAnimation == null && closeAnimation == null) {
                if (initialState == MechanismEnum.DoorState.OPEN) Close();
                else                                              Open();
            }

            else {
                if(openAnimation == null)
                    Open();
                else
                    Close();
            }
        }
    

        private void UpdateButtonState(GameObject button, bool state) {
            foreach (ButtonConnection buttonConnection in buttonConnections)
                buttonConnection.UpdateButtonState(button, state);
            
            CheckForAction();
        }

        private void CheckForAction() {
            foreach (ButtonConnection buttonConnection in buttonConnections)
                switch(buttonConnection.GetAction()) {
                    case MechanismEnum.DoorAction.OPEN:   
                        if(buttonConnection.IsActive()) Open();
                        //else                            Close();
                    break;
                    case MechanismEnum.DoorAction.CLOSE:   
                        if(buttonConnection.IsActive()) Close();
                        //else                            Close();
                    break;
                    case MechanismEnum.DoorAction.TOGGLE: Toggle(); break;   
                }
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
        public MechanismEnum.DoorAction GetAction() { return this.action; }
    }

    [System.Serializable]
    public class ButtonTracker {
        [SerializeField]
        private GameObject button;
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

