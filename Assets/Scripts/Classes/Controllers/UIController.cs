using Shard.Enums;
using Shard.Patterns.Singleton;
using Shard.Gameflow;
using Shard.Lib.Custom;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Shard.Controllers 
{
    public class UIController : SingletonUnity
    {
        private static UIController instance = null;
        public static new UIController Instance { get { return (UIController) instance; }}

        [SerializeField]
        private GameObject programmingUI;
        [SerializeField]
        private GameObject pauseUI;
    
        [SerializeField]
        private Button turnOnButton;
        [SerializeField]
        private Button turnOffButton;

        private Animator animatorPUI;

        private bool isPUIOn;
        

        private void Awake() {
            // Init the controller's instance
            instance = this;

            // Activate the programming UI
            programmingUI.SetActive(true);

            // Get the PUI's components
            animatorPUI = programmingUI.GetComponent<Animator>();
        }

        private void OnEnable() {
            GameFlowManagement.pauseEvent += TogglePauseUI;
            GameFlowManagement.unPauseEvent += TogglePauseUI;
        }

        private void OnDisable() {
            GameFlowManagement.pauseEvent -= TogglePauseUI;
            GameFlowManagement.unPauseEvent -= TogglePauseUI;
        }

        public void ToggleProgrammingUI() {
            string sfxToPlay = IsPUIOn() ? "ClosePUI" : "OpenPUI"; 
            AudioController.Instance.Play(sfxToPlay);

            ToggleUI(UIEnum.UIType.PROGRAMMING_UI);

            isPUIOn = !isPUIOn;
        }

        public void TogglePauseUI() { ToggleUI(UIEnum.UIType.PAUSE_UI); }

        private void ToggleUI(UIEnum.UIType type) {
            switch(type) {
                case UIEnum.UIType.PROGRAMMING_UI: animatorPUI.SetTrigger("togglePUI"); break;
                case UIEnum.UIType.PAUSE_UI: pauseUI.SetActive(!pauseUI.activeInHierarchy); break;

                default: break;
            }
        }
    
        public void TurnOnButtonClick() { turnOnButton.onClick?.Invoke(); }

        public void TurnOffButtonClick() { turnOffButton.onClick?.Invoke(); }

        public bool IsPUIOn() { return this.isPUIOn; }
    }
}
