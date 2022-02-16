using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Shard.UI.ProgrammingUI
{
    public class CommandHistory : MonoBehaviour
    {
        private TextMeshProUGUI historyText;

        private List<string> commandHistory = new List<string>();
        private List<string> resultHistory = new List<string>();


        private void Awake() {
            historyText = this.GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable() {
            InputConsole.submitCommandEvent += RecordCommand;
        }

        private void OnDisable() {
            InputConsole.submitCommandEvent -= RecordCommand;
        }


        private void RecordCommand(string command, string result) {
            commandHistory.Add(command);
            resultHistory.Add(result);

            UpdateHistoryText();
        }

        private void UpdateHistoryText() {
            string historyText = "";

            for (int i = 0; i < commandHistory.Count; i++) {
                historyText += commandHistory[i] + "\n";
                historyText += "\t" + resultHistory[i] + "\n";
            }

            this.historyText.text = historyText;
        } 
    }
}


