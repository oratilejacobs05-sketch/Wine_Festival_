using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine_Festival_project
{
    //Custom exception
    public class FestivalCapacityException : Exception
    {
        public int CurrentAttendees { get; }
        public int MaxCapacity { get; }

        //maximum capacity exception constructor
        public FestivalCapacityException(int current, int max)
            : base($"Venue capacity exceeded! Current capacity is: {current}/{max}. Sysytem Cannot check in more attendees.")
        {
            CurrentAttendees = current;
            MaxCapacity = max;
        }
    }
  
}