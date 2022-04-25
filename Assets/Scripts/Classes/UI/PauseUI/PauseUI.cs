using Shard.Gameflow;
using Shard.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Shard.UI.PauseUI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainOptions;
        [SerializeField]
        private GameObject exitOptions;

        public void ToggleOption(GameObject option) {
            Color optionColor = option.GetComponent<TextMeshProUGUI>().color;

            option.GetComponent<TextMeshProUGUI>().color = optionColor == Color.cyan ? Color.white : Color.cyan;
        }

        public void ExecuteOption(PauseOption option) {
            switch(option.GetOptionType()) {
                case UIEnum.PauseOptionType.RESUME: GameFlowManagement.Pause(); break;
                case UIEnum.PauseOptionType.HELP:   break;
                case UIEnum.PauseOptionType.EXIT:   mainOptions.SetActive(false); exitOptions.SetActive(true); break;
            }
        }

        public void ConfirmExit() { Application.Quit(); }
        public void DismissExit() { mainOptions.SetActive(true); exitOptions.SetActive(false); }
        
    }
}


