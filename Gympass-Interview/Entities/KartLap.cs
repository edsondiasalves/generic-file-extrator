using Gympass_Interview.Extrators;
using System;

namespace Gympass_Interview.Entities
{
    public class KartLap
    {
        [PositionalDataExtract(0, 12)]
        public TimeSpan Time { get; set; }
        [PositionalDataExtract(18, 3)]
        public string PilotCode { get; set; }
        [PositionalDataExtract(24, 15)]
        public string PilotName { get; set; }
        [PositionalDataExtract(58, 1)]
        public int LapNumber { get; set; }
        [PositionalDataExtract(61, 12)]
        public TimeSpan LapTime { get; set; }
        [PositionalDataExtract(93, 0)]
        public decimal LapAverageSpeed { get; set; }
    }
}
