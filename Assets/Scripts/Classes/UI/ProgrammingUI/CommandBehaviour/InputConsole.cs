using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Shard.UI.ProgrammingUI
{
    public class InputConsole : MonoBehaviour
    {
        public static Action enterInputStateEvent;
        public static Action exitInputStateEvent;

        public static Action <string, string>         submitCommandEvent;
        public static Action <string, string, string> generateCommandBehaviourEvent;

        private TMP_InputField inputField;


        private void Awake() {
            inputField = this.GetComponent<TMP_InputField>();

            inputField.onSubmit.AddListener( 
                (string input) => {
                    EnterCommand();
                }
            );
        }

        public void EnterInputState() {
            enterInputStateEvent?.Invoke();
        }

        public void ExitInputState() {
            exitInputStateEvent?.Invoke();
        }


        public void EnterCommand() {
            inputField.text = inputField.text.Substring(0, inputField.text.Length);

            SubmitCommand();

            inputField.text = "";

            inputField.ActivateInputField();
            inputField.Select();
        }  

        private void SubmitCommand() {   
            string command = inputField.text.ToLower();
            string commandEvent = "";
            string commandTrigger = "";
            string commandDelay = "";

            // Parse the command
            string result = "";
            bool isValid = CommandParser.ParseCommand(command, out result, out commandEvent, out commandTrigger, out commandDelay);
            
            // Record it in the history
            submitCommandEvent?.Invoke(command, result);

            // Create the command behaviour
            if(isValid)
                generateCommandBehaviourEvent?.Invoke(commandEvent, commandTrigger, commandDelay);
        }
    }
}


