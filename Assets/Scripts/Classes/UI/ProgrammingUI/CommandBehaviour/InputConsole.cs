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
            string command = inputField.text;
            
            // Parse the command
            string result = ParseCommand(command);
            
            // Record it in the history
            SubmitCommand(command, result);

            // Create the command behaviour
            // CreateCommandBehavior();

        }

        private string ParseCommand(string command) {
            return "result";
        }

        private void SubmitCommand(string command, string result) {
            submitCommandEvent?.Invoke(command, result);
        }
    }
}


