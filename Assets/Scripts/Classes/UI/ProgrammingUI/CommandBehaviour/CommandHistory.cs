using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Shard.UI.ProgrammingUI
{
    public class CommandHistory : MonoBehaviour
    {
        [SerializeField]
        private GameObject commandPrefab;


        private TextMeshProUGUI historyText;

        private List<string> commandHistory = new List<string>();


        private void Awake() {
            historyText = this.GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable() {
            InputConsole.submitCommandEvent      += RecordCommand;
            InputConsole.getPreviousCommandEvent += GetCommand;
            InputConsole.getNextCommandEvent     += GetCommand;
        }

        private void OnDisable() {
            InputConsole.submitCommandEvent      -= RecordCommand;
            InputConsole.getPreviousCommandEvent -= GetCommand;
            InputConsole.getNextCommandEvent     -= GetCommand;
        }


        private void RecordCommand(string command, string result) {
            GameObject commandGO = InstantiateCommand();
            StyleCommand(commandGO, command, result);

            commandHistory.Add(command);
        }

        private GameObject InstantiateCommand() {
            GameObject commandGO = Object.Instantiate(commandPrefab);

            commandGO.name = "command_" + commandHistory.Count;
            commandGO.transform.SetParent(this.transform.GetChild(0));

            return commandGO;
        }

        private void StyleCommand(GameObject commandGO, string command, string result) {
            GameObject commandTextGO = commandGO.transform.GetChild(0).gameObject;
            commandTextGO.GetComponent<TextMeshProUGUI>().SetText(command);

            GameObject resultTextGO = commandGO.transform.GetChild(1).gameObject;
            resultTextGO.GetComponent<TextMeshProUGUI>().SetText(result);
        }

        public string GetCommand(int index) {
            if(index < 0)                           return this.commandHistory[0];
            else if (index >= commandHistory.Count) return "";
                                                    return this.commandHistory[index];
        }
    }
}


