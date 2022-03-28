using Shard.Enums;
using Shard.Entities;
using System;
using System.Collections;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class CommandBehaviour : MonoBehaviour
    {
        private EntityEnum.Action commandEventAction;
        public static Action commandEvent;

        private CommandEnum.TriggerInvoker commandTriggerInvoker;
        private EntityEnum.Action commandTriggerAction;
        public static Action commandTrigger;

        private float commandDelay;


        private void OnEnable() {
            commandTrigger += ExecuteCommand;
        }

        private void OnDisable() {
            commandTrigger = null;
            commandEvent = null;
        }


        public void CreateCommandBehaviour(string commandEventAction, string commandTrigger, string commandDelay) {
            SetCommandEvent(commandEventAction.ToUpper());
            SetCommandTrigger(commandTrigger.ToUpper());

            if(commandDelay != "")
                SetCommandDelay(commandDelay);
        }

        private void SetCommandEvent(string commandEventAction) {
            this.commandEventAction = (EntityEnum.Action) System.Enum.Parse(typeof(EntityEnum.Action), commandEventAction);
        }

        private void SetCommandTrigger(string commandTrigger) {
            string[] split = commandTrigger.Split('_');

            this.commandTriggerInvoker =
                split.Length > 1 ?
                    (CommandEnum.TriggerInvoker) System.Enum.Parse(typeof(CommandEnum.TriggerInvoker), split[0]) :
                    CommandEnum.TriggerInvoker.ROBOT;

            this.commandTriggerAction =
                split.Length > 1 ?
                    (EntityEnum.Action) System.Enum.Parse(typeof(EntityEnum.Action), split[1]) :
                    (EntityEnum.Action) System.Enum.Parse(typeof(EntityEnum.Action), commandTrigger);
        }

        private void SetCommandDelay(string commandDelay) {
            this.commandDelay = float.Parse(commandDelay);
        }


        private void ExecuteCommand() {
            StartCoroutine(CommandCoroutine());
        }

        private IEnumerator CommandCoroutine() {
            yield return new WaitForSeconds(commandDelay);

            commandEvent?.Invoke();
        }


        public EntityEnum.Action GetCommandEventAction()
        {
            return this.commandEventAction;
        }

        public void SetCommandEventAction(EntityEnum.Action commandEventAction)
        {
            this.commandEventAction = commandEventAction;
        }

        public CommandEnum.TriggerInvoker GetCommandTriggerInvoker()
        {
            return this.commandTriggerInvoker;
        }

        public void SetCommandTriggerInvoker(CommandEnum.TriggerInvoker commandTriggerInvoker)
        {
            this.commandTriggerInvoker = commandTriggerInvoker;
        }

        public EntityEnum.Action GetCommandTriggerAction()
        {
            return this.commandTriggerAction;
        }

        public void SetCommandTriggerAction(EntityEnum.Action commandTriggerAction)
        {
            this.commandTriggerAction = commandTriggerAction;
        }

        public float GetCommandDelay()
        {
            return this.commandDelay;
        }

        public void SetCommandDelay(float commandDelay)
        {
            this.commandDelay = commandDelay;
        }
    }

}

