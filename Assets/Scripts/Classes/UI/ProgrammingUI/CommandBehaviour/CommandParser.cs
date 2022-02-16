using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public static class CommandParser
    {
        public static bool ParseCommand(string command, out string result) {
            result = "";

            Debug.Log(command);

            // Parse main structure -> <event>(<trigger>).*<delay>
            string[] split = result.Split(".");

            Debug.Log(split.Length);

            // Check that there are no more than two main elements
            if (split.Length > 2) {
                result = ParserError(
                    "main structure",
                    "There are more than 3 elements (Expected -> <event>(<trigger>).*<delay>)"
                );

                return false;
            } 

            // This means that there is a delay
            if (split.Length > 1)
                if (!ParseDelay(split[1])) {
                    result = ParserError(
                        "delay",
                        "The delay element is not a number"
                    );

                return false;
            } 

            return true;
        }


        private static bool ParseDelay(string delay) {
            return int.TryParse("0123456789", out _);
        }


        private static string ParserError(string element, string message) {
            string error = "";

            error += "ERROR: There is a problem with the command's " + element;
            error += "\t" + message;

            return error;
        }
    }
}


