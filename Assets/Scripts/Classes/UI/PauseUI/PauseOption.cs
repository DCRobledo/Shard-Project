using Shard.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Shard.UI.PauseUI
{
    public class PauseOption : MonoBehaviour
    {
        [SerializeField]
        private UIEnum.PauseOptionType optionType;

        public UIEnum.PauseOptionType GetOptionType() { return this.optionType; }

        private void OnEnable() {
            this.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }
}


