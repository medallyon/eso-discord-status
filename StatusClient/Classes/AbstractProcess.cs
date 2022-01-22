using System;
using System.Diagnostics;
using System.Management;

namespace DiscordStatus
{
    public class AbstractProcess : IProcess
    {
        public Process Process
        {
            get
            {
                var pName = Process.GetProcessesByName(ProcessName);
                return pName.Length > 0 ? pName[0] : null;
            }
        }

        public bool Exists => Process != null;

        protected string ProcessName;

        protected AbstractProcess() { }
        public AbstractProcess(string pName)
        {
            ProcessName = pName;
        }
    }
}