using Shard.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandBehaviour : MonoBehaviour
{
    public enum CommandAction {
        JUMP,
        WALK,
        FLIP,
        GRAB,
        THROW,
        STOP
    }

    public enum TriggerInvoker {
        LILY,
        ROBOT
    }

    private CommandAction commandEventAction;
    public static Action commandEvent;

    private TriggerInvoker commandTriggerInvoker;
    private CommandAction commandTriggerAction;
    public static Action commandTrigger;

    private float commandDelay;


    private void OnEnable() {
        commandTrigger += commandEvent.Invoke;
    }

    private void OnDisable() {
        commandEvent = null;
        commandTrigger = null;
    }


    public void CreateCommandBehaviour(string commandEventAction, string commandTrigger, string commandDelay) {
        SetCommandEvent(commandEventAction.ToUpper());
        SetCommandTrigger(commandTrigger.ToUpper());

        if(commandDelay != "")
            SetCommandDelay(commandDelay);
    }

    private void SetCommandEvent(string commandEventAction) {
        this.commandEventAction = (CommandAction) System.Enum.Parse(typeof(CommandAction), commandEventAction);
    }

    private void SetCommandTrigger(string commandTrigger) {
        string[] split = commandTrigger.Split('_');
        string commandTriggerInvoker = split[0];
        string commandTriggerAction = split[1];

        this.commandTriggerInvoker = (TriggerInvoker) System.Enum.Parse(typeof(TriggerInvoker), commandTriggerInvoker);
        this.commandTriggerAction = (CommandAction) System.Enum.Parse(typeof(CommandAction), commandTriggerAction);
    }

    private void SetCommandDelay(string commandDelay) {
        this.commandDelay = float.Parse(commandDelay);
    }


    public CommandAction GetCommandEventAction()
    {
        return this.commandEventAction;
    }

    public void SetCommandEventAction(CommandAction commandEventAction)
    {
        this.commandEventAction = commandEventAction;
    }

    public TriggerInvoker GetCommandTriggerInvoker()
    {
        return this.commandTriggerInvoker;
    }

    public void SetCommandTriggerInvoker(TriggerInvoker commandTriggerInvoker)
    {
        this.commandTriggerInvoker = commandTriggerInvoker;
    }

    public CommandAction GetCommandTriggerAction()
    {
        return this.commandTriggerAction;
    }

    public void SetCommandTriggerAction(CommandAction commandTriggerAction)
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
