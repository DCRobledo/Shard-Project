using Shard.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandBehaviour : MonoBehaviour
{
    public enum CommandEvent {
        JUMP,
        WALK,
        FLIP,
        GRAB,
        THROW,
        STOP
    }
    private CommandEvent commandEvent;

    public enum TriggerInvoker {
        LILY,
        ROBOT
    }

    private TriggerInvoker commandTriggerInvoker;
    private CommandEvent commandTriggerAction;

    private float commandDelay;

    public void CreateCommandBehaviour(string commandEvent, string commandTrigger, string commandDelay) {
        SetCommandEvent(commandEvent.ToUpper());
        SetCommandTrigger(commandTrigger.ToUpper());

        if(commandDelay != "")
            SetCommandDelay(commandDelay);
    }

    private void SetCommandEvent(string commandEvent) {
        this.commandEvent = (CommandEvent) System.Enum.Parse(typeof(CommandEvent), commandEvent);
    }

    private void SetCommandTrigger(string commandTrigger) {
        string[] split = commandTrigger.Split('_');
        string commandTriggerInvoker = split[0];
        string commandTriggerAction = split[1];

        this.commandTriggerInvoker = (TriggerInvoker) System.Enum.Parse(typeof(TriggerInvoker), commandTriggerInvoker);
        this.commandTriggerAction = (CommandEvent) System.Enum.Parse(typeof(CommandEvent), commandTriggerAction);
    }

    private void SetCommandDelay(string commandDelay) {
        this.commandDelay = float.Parse(commandDelay);
    }
}
