using Gympass_Interview.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gympass_Interview.Services
{
    class ComputeRankingKartRaceService : IComputeRankingKartRaceService
    {   
        private List<KartRaceResult> Ranking { get; set; }

        public ComputeRankingKartRaceService()
        {
            this.Ranking = new List<KartRaceResult>();
        }

        public List<KartRaceResult> ComputeRanking(List<KartLap> kartLaps)
        {
            if (kartLaps == null || kartLaps.Count < 1)
            {
                return new List<KartRaceResult>();
            }

            this.Ranking = 
                (from lap in kartLaps
                     group lap by lap.PilotCode into laps
                        orderby laps.Key
                        select new KartRaceResult
                        {
                            PilotCode = laps.Key,
                            PilotName = laps.Where(w => w.PilotCode == laps.Key).First().PilotName,
                            Lap = laps
                                        .Where(w => w.PilotCode == laps.Key)
                                        .Max(s => s.LapNumber),
                            TotalTime = new TimeSpan(laps
                                        .Where(w => w.PilotCode == laps.Key)
                                        .Sum(s => s.LapTime.Ticks)),
                            BestLapNumber = laps
                                        .Where(w => w.PilotCode == laps.Key)
                                        .OrderBy(s => s.LapTime)
                                        .First()
                                        .LapNumber,
                            BestLapTime = laps
                                        .Where(w => w.PilotCode == laps.Key)
                                        .OrderBy(s => s.LapTime)
                                        .First()
                                        .LapTime,
                            AverageSpeed = laps
                                        .Where(w => w.PilotCode == laps.Key)
                                        .Average(s => s.LapAverageSpeed)

                        })
                        .OrderByDescending(o => o.Lap)
                        .OrderBy(b => b.TotalTime)
                        .ToList();

            return this.Ranking;
        }

        public KartRaceResult GetBestLap()
        {
            if (this.Ranking.Count > 0)
                return this.Ranking.OrderBy(o => o.BestLapTime).First();
            else
                return new KartRaceResult();
        }
    }
}
