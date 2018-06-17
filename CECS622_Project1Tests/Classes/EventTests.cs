using Microsoft.VisualStudio.TestTools.UnitTesting;
using CECS622_Project1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS622_Project1.Classes.Tests
{
    [TestClass()]
    public class EventTests
    {
        [TestMethod()]
        public void EventTest()
        {
            const int testTime = 42;
            Event testEvent = new Event(Event.eventType.Arrival, testTime);

            Assert.IsTrue(testEvent.Type == Event.eventType.Arrival);
            Assert.IsTrue(testEvent.Time == testTime);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Event testEventEarly = new Event(Event.eventType.Arrival, 42);
            Event testEventLate = new Event(Event.eventType.Departure, 736);

            Assert.IsTrue(testEventEarly.CompareTo(testEventLate) < 0);
        }
    }
}