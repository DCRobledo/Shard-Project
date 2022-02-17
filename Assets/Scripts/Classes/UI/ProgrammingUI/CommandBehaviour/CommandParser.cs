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
            string[] split = command.Split(".");

            // Check that there are no more than two main elements
            if (!ParseMainStructure(command, out split, out result)) return false;


            // Parse the delay, if it exists
            if (split.Length > 1){
                string commandDelay = split[1];

                if (!ParseDelay(commandDelay, out result)) return false; 
            }
                

            // Now, check that the trigger is surroned by brackets
            if (!ParseTriggersBrackets(split[0], out split, out result)) return false;


            // Parse the event
            string commandEvent = split[0];

            if (!ParseEvent(commandEvent, out result)) return false;


            // Parse the trigger
            string commandTrigger = split[1];

            if (!ParseTrigger(commandTrigger, out result)) return false;
    

            // Parse OK
            result = "OK: A new command behaviour has been created"; 
            return true;
        }


        private static bool ParseTriggersBrackets(string command, out string[] split, out string result) {
            split = command.Split('(');

            // This contemplates that the trigger element is doesn't have opening bracket
            if(split.Length < 2) {
                result = ParserError(
                    "main structure",
                    "The event and trigger elements are not differentiated"
                );

                return false;
            }

            // This contemplates that the trigger element is doesn't have ending bracket
            string[] secondSplit = split[1].Split(')');

            if(split.Length < 2) {
                result = ParserError(
                    "main structure",
                    "The event and trigger elements are not differentiated"
                );

                return false;
            }

            result = "";
            return true;
        }

        private static bool ParseMainStructure(string command, out string[] split, out string result) {
            split = command.Split(".");
            bool parseOk = split.Length <= 2;
            
            result = parseOk ? "" : ParserError(
                                        "main structure",
                                        "There are more than 3 elements"
                                    );

            return parseOk;
        }

        private static bool ParseDelay(string commandDelay, out string result) {
            bool parseOk = int.TryParse(commandDelay, out _);

            result = parseOk ? "" : ParserError(
                                        "delay",
                                        "The delay element is not a number"
                                    );

            return parseOk;
        }

        private static bool ParseEvent(string commandEvent, out string result) {
            bool parseOk = Array.IndexOf(possibleEvents, commandEvent) > -1;

            result = parseOk ? "" : ParserError(
                                        "delay",
                                        "The delay element is not a number"
                                    );

            return parseOk;
        }

        private static bool ParseTrigger(string commandTrigger, out string result) {
            bool parseOk = Array.IndexOf(possibleTriggers, commandTrigger) > -1;

            result = parseOk ? "" : ParserError(
                                        "delay",
                                        "The delay element is not a number"
                                    );

            return parseOk;
        }


        private static string ParserError(string element, string message) {
            string error = "";

            error += "ERROR: There is a problem with the command's " + element + "\n";
            error += "\t" + message;

            return error;
        }
    }
}


