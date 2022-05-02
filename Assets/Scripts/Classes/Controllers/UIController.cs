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
        private GameObject helpUI;
    
        [SerializeField]
        private Button turnOnButton;
        [SerializeField]
        private Button turnOffButton;

        private Animator animatorPUI;

        private bool isPUIOn;
        

        private void Awake() {
            // Init the controller's instance
            instance = this;
        }

        private void Start() {
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


        private void ToggleUI(UIEnum.UIType type) {
            switch(type) {
                case UIEnum.UIType.PROGRAMMING_UI: ToggleProgrammingUI(); break;
                case UIEnum.UIType.PAUSE_UI:  TogglePauseUI(); break;
                case UIEnum.UIType.HELP_UI: ToggleHelpUI(); break;

                default: break;
            }
        }

        public void ToggleProgrammingUI() {
            animatorPUI.SetTrigger("togglePUI");

            string sfxToPlay = IsPUIOn() ? "ClosePUI" : "OpenPUI"; 
            AudioController.Instance.Play(sfxToPlay);

            isPUIOn = !isPUIOn;

            if(IsHelpUIOn())
                ToggleHelpUI();
        }
        
        public void TogglePauseUI() {
            pauseUI.SetActive(!pauseUI.activeInHierarchy);
        }
        
        public void ToggleHelpUI() {
            helpUI.SetActive(!helpUI.activeInHierarchy);
        }


        public void TurnOnButtonClick() { turnOnButton.onClick?.Invoke(); }
        
        public void TurnOffButtonClick() { turnOffButton.onClick?.Invoke(); }


        public bool IsPUIOn() { return this.isPUIOn; }
        
        public bool IsHelpUIOn() { return this.helpUI.activeInHierarchy; }
    }
}
