using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine_Festival_project
{
    internal static class Events
    {
        public delegate void SystemEventHandler(string message);


        public static event SystemEventHandler OnAlertTriggered;
        public static event SystemEventHandler OnTaskCompleted;
    }
}
