using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shard.Lib.Custom
{
    public class BuildConsole : MonoBehaviour
    {
        #if !UNITY_EDITOR

        static string myLog = "";
        private string output;
        private string stack; 

        private void Start() {
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        public void Log(string logString, string stackTrace, LogType type)
        {
            output = logString;
            stack = stackTrace;
            myLog = output + "\n" + myLog;

            if (myLog.Length > 5000)
                myLog = myLog.Substring(0, 4000);
        }

        void OnGUI()
        {
            myLog = GUI.TextArea(new Rect(10, 980, Screen.width - 100, Screen.height - 980), myLog);
        }

        #endif
    }
}
