using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gympass_Interview.Entities
{
    public class KartRaceResult
    {
        public string PilotCode { get; set; }
        public string PilotName { get; set; }
        public int Lap { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int BestLapNumber { get; set; }
        public TimeSpan BestLapTime { get; set; }
        public Decimal AverageSpeed { get; set; }
    }
}
