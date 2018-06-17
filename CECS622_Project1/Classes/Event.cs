using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS622_Project1.Classes
{
    public class Event : IComparable
    {
        public enum eventType { Arrival, Transfer, Departure, End };

        public double Time { get; }
        public eventType Type { get; }

        public Event (eventType type, double time)
        {
            Type = type;
            Time = time;
        }

        public int CompareTo(object obj)
        {
            if (obj is Event)
            {
                return Time.CompareTo((obj as Event).Time);
            }

            throw new ArgumentException("Object 'Event' may only be compared to other objects of type 'Event'");
        }
    }
}
 