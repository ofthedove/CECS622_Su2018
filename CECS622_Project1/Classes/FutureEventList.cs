using System;
using System.Collections.Generic;
using System.Linq;

namespace CECS622_Project1.Classes
{
    public class FutureEventList
    {
        private List<Event> eventList = new List<Event>();

        public FutureEventList (double endTime)
        {
            Event endEvent = new Event(Event.eventType.End, endTime);
            this.Add(endEvent);
        }

        public void Add (Event newEvent)
        {
            eventList.Add(newEvent);
            eventList.Sort();
        }

        public Event PopNextEvent ()
        {
            if (eventList.Count < 1)
            {
                throw new FEListEmptyException();
            }

            Event nextEvent = eventList.First();

            eventList.RemoveAt(0);

            return nextEvent;
        }

        public double NextEventTime()
        {
            if (eventList.Count < 1)
            {
                throw new FEListEmptyException();
            }

            return eventList.First().Time;
        }

        public override string ToString()
        {
            string returnString = "";

            foreach(Event e in eventList)
            {
                returnString += e.Type.ToString()[0] + " " + e.Time.ToString("0.0000").PadLeft(8, ' ') + "   ";
            }

            return returnString;
        }

        public class FEListEmptyException : ApplicationException { }
    }
}
