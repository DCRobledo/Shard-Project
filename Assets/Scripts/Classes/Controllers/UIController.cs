using Shard.Enums;
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

        private GameObject programmingUI;
        private Animator animatorPUI;
        

        private void Awake() {
            // Init the controller's instance
            instance = this;

            // Get the programming UI
            programmingUI = GameObject.Find("programmingUI");

            // Get the PUI's components
            animatorPUI = programmingUI.GetComponent<Animator>();
        }


        public void ToggleProgrammingUI() { ToggleUI(UIEnum.UIType.PROGRAMMING_UI); }

        private void ToggleUI(UIEnum.UIType type) {
            switch(type) {
                case UIEnum.UIType.PROGRAMMING_UI: animatorPUI.SetTrigger("togglePUI"); break;

                default: break;
            }
        }
    }
}
