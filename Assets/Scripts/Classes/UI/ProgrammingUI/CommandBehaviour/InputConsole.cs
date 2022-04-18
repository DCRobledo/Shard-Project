using Shard.Input;
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

        public static Func <int, string> getPreviousCommandEvent;
        public static Func <int, string> getNextCommandEvent;

        private int currentCommandIndex;
        private int numOfCommands;

        private TMP_InputField inputField;

        private InputActions inputActions;


        private void Awake() {
            inputField = this.GetComponent<TMP_InputField>();

            inputField.onSubmit.AddListener( 
                (string input) => {
                    EnterCommand();
                }
            );

            inputActions = new InputActions();
        }

        private void OnEnable() {
            inputActions.CommandConsole.PreviousCommand.performed += context => GetPreviousCommand();
            inputActions.CommandConsole.NextCommand.performed     += context => GetNextCommand();
        }
        
        private void OnDisable() {
            inputActions.CommandConsole.PreviousCommand.performed -= context => GetPreviousCommand();
            inputActions.CommandConsole.NextCommand.performed     -= context => GetNextCommand();
        }


        public void EnterInputState() {
            enterInputStateEvent?.Invoke();

            inputActions.CommandConsole.Enable();
        }

        public void ExitInputState() {
            exitInputStateEvent?.Invoke();

            inputActions.CommandConsole.Disable();
        }


        public void EnterCommand() {
            numOfCommands++;
            currentCommandIndex++;

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
    
        private void GetPreviousCommand() {
            if(currentCommandIndex > 0) currentCommandIndex--;

            inputField.text = getPreviousCommandEvent?.Invoke(currentCommandIndex);
        }

        private void GetNextCommand() {
            if(currentCommandIndex < numOfCommands) currentCommandIndex++;

            inputField.text = getNextCommandEvent?.Invoke(currentCommandIndex);
        }
    }
}


