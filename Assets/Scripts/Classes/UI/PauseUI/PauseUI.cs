using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Shard.UI.PauseUI
{
    public class PauseUI : MonoBehaviour
    {
        public void ToggleOption(GameObject option) {
            Color optionColor = option.GetComponent<TextMeshProUGUI>().color;

            option.GetComponent<TextMeshProUGUI>().color = optionColor == Color.cyan ? Color.white : Color.cyan;
        }
    }
}


