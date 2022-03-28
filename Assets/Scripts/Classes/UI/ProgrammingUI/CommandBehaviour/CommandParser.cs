using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public static class CommandParser
    {
        private static string[] possibleEvents = {
            "jump",
            "move",
            "flip",
        };

        private static string[] possibleTriggers = {
            "lily_jump",
            "lily_move",
            "lily_flip",
            "lily_crouch",
            "lily_grab",
            "lily_drop",

            "jump",
            "move",
            "flip",

            "turn_on"
        };


        public static bool ParseCommand(string command, out string result, out string commandEvent, out string commandTrigger, out string commandDelay) {
            string[] split = command.Split(".");
            commandEvent = "";
            commandTrigger = "";
            commandDelay = "";

            // Check that there are no more than two main elements
            if (!ParseMainStructure(command, out split, out result)) return false;


            // Parse the delay, if it exists
            if (split.Length > 1){
                commandDelay = split[1];

                if (!ParseDelay(commandDelay, out result)) return false; 
            }
                

            // Now, check that the trigger is surroned by brackets
            if (!ParseTriggersBrackets(split[0], out split, out result)) return false;


            // Parse the event
             commandEvent = split[0];

            if (!ParseEvent(commandEvent, out result)) return false;


            // Parse the trigger
             commandTrigger = split[1].Split(')')[0];

            if (!ParseTrigger(commandTrigger, out result)) return false;
    

            // Parse OK
            result = "OK: A new command behaviour has been created"; 
            return true;
        }


        private static bool ParseTriggersBrackets(string command, out string[] split, out string result) {
            split = command.Split('(');

            // This contemplates that the trigger element is doesn't have opening bracket
            if(split.Length < 2) {
                result = "ERROR: The event and trigger elements are not differentiated";

                return false;
            }

            // This contemplates that the trigger element is doesn't have ending bracket
            string[] secondSplit = split[1].Split(')');

            if(secondSplit.Length < 2) {
                result = "ERROR: The event and trigger elements are not differentiated";

                return false;
            }

            result = "";
            return true;
        }

        private static bool ParseMainStructure(string command, out string[] split, out string result) {
            split = command.Split(".");
            bool parseOk = split.Length <= 2;

            result = parseOk ? "" : "ERROR: There are more than 3 elements";

            return parseOk;
        }

        private static bool ParseDelay(string commandDelay, out string result) {
            bool parseOk = float.TryParse(commandDelay.Replace(',', '.'), out _);

            result = parseOk ? "" : "ERROR: The delay element is not a number";

            return parseOk;
        }

        private static bool ParseEvent(string commandEvent, out string result) {
            bool parseOk = Array.IndexOf(possibleEvents, commandEvent) > -1;

            result = parseOk ? "" : "ERROR: The event element is not a valid option";

            return parseOk;
        }

        private static bool ParseTrigger(string commandTrigger, out string result) {
            bool parseOk = Array.IndexOf(possibleTriggers, commandTrigger) > -1;

            result = parseOk ? "" : "ERROR: The trigger element is not a valid option";

            return parseOk;
        }
    }
}


