using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace CECS622_Project1.Classes
{
    class Facility
    {
        // ----- Parameters
        const double IAT_MEAN = 1;      // Inter Arrival Time
        const double ST1_MEAN = 0.7;    // Service Time Server 1
        const double ST2_MEAN = 0.9;    // Service Time Server 2
        const double IAT_RATE = 1.0D / IAT_MEAN; // Lambda (rate) is one over mean
        const double ST1_RATE = 1.0D / ST1_MEAN;
        const double ST2_RATE = 1.0D / ST2_MEAN;
        const int END_TIME = 1000;      // Simulation run time

        // ----- Facility State
        private FutureEventList FEL;
        private int queue1 = 0;
        private int queue2 = 0;
        private bool server1 = false;
        private bool server2 = false;
        private double curTime = 0;

        // ----- Random Generators
        private Exponential rGenArrival = new Exponential(IAT_RATE);
        private Exponential rGenServer1 = new Exponential(ST1_RATE);
        private Exponential rGenServer2 = new Exponential(ST2_RATE);

        // ----- Statistics
        double s1_utilTime = 0;
        double s2_utilTime = 0;
        double q1_maxLen = 0;
        double q2_maxLen = 0;
        double q1_waitTime = 0;
        double q2_waitTime = 0;
        int numDepartures = 0;

        double Server1Utilization { get { return s1_utilTime / (double)END_TIME; } }
        double Server2Utilization { get { return s2_utilTime / (double)END_TIME; } }
        double MaxLengthQueue1 { get { return q1_maxLen; } }
        double MaxLengthQueue2 { get { return q2_maxLen; } }
        double AverageWaitQueue1 { get { return q1_waitTime / (double)numDepartures; } }
        double AverageWaitQueue2 { get { return q2_waitTime / (double)numDepartures; } }
        int NumberDepartures { get { return numDepartures; } }

        public Facility()
        {
            FEL = new FutureEventList(END_TIME);
        }

        public void RunSimulation()
        {
            bool running = true;
            double deltaTime = 0;

            // pre-load first arrival

            while (running)
            {
                // Jump to next event
                deltaTime = FEL.NextEventTime();
                curTime += deltaTime;

                // Update Statisitcs
                s1_utilTime += server1 ? deltaTime : 0;
                s2_utilTime += server2 ? deltaTime : 0;
                q1_maxLen = queue1 > q1_maxLen ? queue1 : q1_maxLen;
                q2_maxLen = queue2 > q2_maxLen ? queue2 : q2_maxLen;
                q1_waitTime += deltaTime * queue1;
                q2_waitTime += deltaTime * queue2;

                // Process next event
                Event curEvent = FEL.PopNextEvent();
                switch (curEvent.Type)
                {
                    case Event.eventType.Arrival:
                        AddCustomer_Server1();
                        AddNextArrival();
                        break;
                    case Event.eventType.Transfer:
                        TryWorkQueue_Server1();
                        AddCustomer_Server2();
                        break;
                    case Event.eventType.Departure:
                        numDepartures++;
                        TryWorkQueue_Server2();
                        break;
                    case Event.eventType.End:
                        running = false;
                        break;
                    default:
                        throw new ApplicationException("Unrecognized event type!");
                }
            }
        }

        private void AddNextArrival()
        {
            double iat = rGenArrival.Sample();
            FEL.Add(new Event(Event.eventType.Arrival, curTime + iat));
        }

        private void AddCustomer_Server1()
        {
            if (!server1)
            {
                server1 = true;
                double workTime = rGenServer1.Sample();
                FEL.Add(new Event(Event.eventType.Transfer, curTime + workTime));
            }
            else
            {
                queue1++;
            }
        }

        private void TryWorkQueue_Server1()
        {
            if (queue1 > 0)
            {
                queue1--;
                server1 = true;
                double workTime = rGenServer1.Sample();
                FEL.Add(new Event(Event.eventType.Transfer, curTime + workTime));
            }
            else
            {
                server1 = false;
            }
        }

        private void AddCustomer_Server2()
        {
            if (!server2)
            {
                server2 = true;
                double workTime = rGenServer2.Sample();
                FEL.Add(new Event(Event.eventType.Departure, curTime + workTime));
            }
            else
            {
                queue2++;
            }
        }

        private void TryWorkQueue_Server2()
        {
            if (queue2 > 0)
            {
                queue2--;
                server2 = true;
                double workTime = rGenServer2.Sample();
                FEL.Add(new Event(Event.eventType.Departure, curTime + workTime));
            }
            else
            {
                server2 = false;
            }
        }
    }
}
