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

        public static Action<string, string> submitCommandEvent;

        private TMP_InputField inputField;


        private void Awake() {
            inputField = this.GetComponent<TMP_InputField>();
        }


        public void EnterInputState() {
            enterInputStateEvent?.Invoke();
        }

        public void ExitInputState() {
            exitInputStateEvent?.Invoke();
        }


        public void EnterCommand() {
            if (inputField.text.EndsWith(';')) {
                inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);

                SubmitCommand();

                inputField.text = "";

                inputField.ActivateInputField();
                inputField.Select();
            } 
        }  

        private void SubmitCommand() {   
            string command = inputField.text.ToLower();

            // Parse the command
            string result = "";
            bool isValid = CommandParser.ParseCommand(command, out result);
            
            // Record it in the history
            submitCommandEvent?.Invoke(command, result);

            // Create the command behaviour
            // CreateCommandBehavior();
        }
    }
}


