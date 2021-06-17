using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PilotAPI.Models
{
    public class Pilot
    {
        public int PilotId { get; set; }
        public string PilotName { get; set; }
        public string Schedule { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
