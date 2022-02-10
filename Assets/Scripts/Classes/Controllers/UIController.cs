using Shard.Patterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Controllers 
{
    public class UIController : SingletonUnity
    {
        private static UIController instance = null;
        public static new UIController Instance { get { return (UIController) instance; }}

        private enum UIType {
            PROGRAMMING_UI,
            HUD
        }

        private GameObject programmingUI;
        

        private void Awake() {
            // Init the controller's instance
            instance = this;

            // Get the programming UI
            programmingUI = GameObject.Find("programmingUI");
        }

        public void ToggleProgrammingUI() { ToggleUI(UIType.PROGRAMMING_UI); }

        private void ToggleUI(UIType type) {
            switch(type) {
                case UIType.PROGRAMMING_UI: programmingUI.SetActive(!programmingUI.activeSelf); break;
                case UIType.HUD:                                                                break;

                default: break;
            }
        }
    }
}
