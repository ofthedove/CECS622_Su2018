using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS622_Project1.Classes
{
    public class StatusItem
    {
        public double ClockTime { get; set; }
        public int Queue1Status { get; set; }
        public bool Server1Status { get; set; }
        public int Queue2Status { get; set; }
        public bool Server2Status { get; set; }
        public string FELContents { get; set; }

        public StatusItem ()
        {

        }
    }
}
