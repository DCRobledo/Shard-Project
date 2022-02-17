using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public static class CommandParser
    {
        private static string[] possibleEvents = {
            "jump",
            "walk",
            "grab",
            "crouch",
            "throw",
        };

        private static string[] possibleTriggers = {
            "lily_jump",
            "lily_walk",
            "lily_clap",
            "lily_grab",
            "lily_crouch",
            "lily_throw",

            "robot_jump",
            "robot_walk",
            "robot_grab",
            "robot_crouch",
            "robot_throw",

            "turn_on"
        };

        public static bool ParseCommand(string command, out string result) {
            result = "";

            // Parse main structure -> <event>(<trigger>).*<delay>
            string[] split = command.Split(".");

            // Check that there are no more than two main elements
            if (split.Length > 2) {
                result = ParserError(
                    "main structure",
                    "There are more than 3 elements"
                );

                return false;
            } 

            // Parse the delay, if it exists
            if (split.Length > 1){
                string commandDelay = split[1];

                if (!ParseDelay(commandDelay)) {
                    result = ParserError(
                        "delay",
                        "The delay element is not a number"
                    );

                    return false;
                } 
            }
                

            // Now, split again into event and trigger
            split = split[0].Split('(');

            // This contemplates that the trigger element is not bracket-surronded
            if(split.Length < 2) {
                result = ParserError(
                    "main structure",
                    "The event and trigger elements are not differentiated"
                );

                return false;
            }

            // Parse the event
            string commandEvent = split[0];

            if (!ParseEvent(commandEvent)) {
                result = ParserError(
                    "event",
                    "This event option is not allowed"
                );

                return false;
            } 


            // Check that the brackets are closed
            string commandTriggerWithBracket = split[1];

            split = commandTriggerWithBracket.Split(')');
            if(split.Length < 2) {
                result = ParserError(
                    "main structure",
                    "The trigger elements is not surronded by curly brackets"
                );

                return false;
            }

            // Parse the trigger
            string commandTrigger = split[0];

            if (!ParseTrigger(commandTrigger)) {
                result = ParserError(
                    "trigger",
                    "This trigger option is not allowed"
                );

                return false;
            }

            // Parse OK
            result = "OK: A new command behaviour has been created"; 
            return true;
        }


        private static bool ParseDelay(string commandDelay) {
            return int.TryParse(commandDelay, out _);
        }

        private static bool ParseEvent(string commandEvent) {
            return Array.IndexOf(possibleEvents, commandEvent) > -1;
        }

        private static bool ParseTrigger(string commandTrigger) {
            return Array.IndexOf(possibleTriggers, commandTrigger) > -1;
        }


        private static string ParserError(string element, string message) {
            string error = "";

            error += "ERROR: There is a problem with the command's " + element + "\n";
            error += "\t" + message;

            return error;
        }
    }
}


