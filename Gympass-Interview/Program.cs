using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gympass_Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sr = File.OpenText("data.txt"))
            {
                List<KartLap> kartLaps = new List<KartLap>();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    KartLap lap = new KartLap();

                    TimeSpan time;
                    if (TimeSpan.TryParse(ExtractData(line, 0, 12), out time))
                        lap.Time = time;
                    else
                        continue;

                    lap.PilotCode = ExtractData(line, 18, 3);
                    lap.PilotName = ExtractData(line, 24, 15);

                    int lapNumber;
                    if (Int32.TryParse(ExtractData(line, 58, 1), out lapNumber))
                        lap.LapNumber = lapNumber;

                    var lapTime = ExtractData(line, 61, 12);
                    var minute = lapTime.Split(':')[0];
                    var second = lapTime.Split('.')[0].Split(':')[1];
                    var milisecond = lapTime.Split('.')[1].Trim();
                    lap.LapTime = new TimeSpan(0, 0, int.Parse(minute), int.Parse(second), int.Parse(milisecond));

                    decimal average;
                    var lapAverageSpeed = ExtractData(line, 93, line.Length - 93).Trim();
                    if (Decimal.TryParse(lapAverageSpeed, out average))
                        lap.LapAverageSpeed = average;

                    kartLaps.Add(lap);
                }

                Console.WriteLine("Time - Code - Name - LapNumber - LapTime - LapAverageSpeed");

                foreach (var a in kartLaps)
                {
                    Console.WriteLine($"" +
                        $"{ a.Time } - " +
                        $"{a.PilotCode} - " +
                        $"{a.PilotName.PadRight(15)} - " +
                        $"{a.LapNumber} - " +
                        $"{a.LapTime} - " +
                        $"{a.LapAverageSpeed.ToString("0.###")}");
                }

                Console.WriteLine("");

                var pilots = (from lap in kartLaps
                              group lap by lap.PilotCode into laps
                              orderby laps.Key
                              select new
                              {
                                  PilotCode = laps.Key,
                                  PilotName = laps.Where(w => w.PilotCode == laps.Key).First().PilotName,
                                  Laps = laps
                                            .Where(w => w.PilotCode == laps.Key)
                                            .Max(s => s.LapNumber),
                                  Total = new TimeSpan(laps
                                            .Where(w => w.PilotCode == laps.Key)
                                            .Sum(s => s.LapTime.Ticks)),
                                  BestLap = laps
                                            .Where(w => w.PilotCode == laps.Key)
                                            .OrderBy(s => s.LapTime)
                                            .First()
                                            .LapNumber,
                                  BestTime = laps
                                            .Where(w => w.PilotCode == laps.Key)
                                            .OrderBy(s => s.LapTime)
                                            .First()
                                            .LapTime,
                                  AverageSpeed = laps
                                            .Where(w => w.PilotCode == laps.Key)
                                            .Average(s => s.LapAverageSpeed)

                              })
                                .OrderByDescending(o => o.Laps)
                                .OrderBy(b => b.Total)
                                .ToList();

                for (var a = 0; a < pilots.Count; a++)
                {
                    Console.WriteLine($"" +
                        $"{a + 1}º - { pilots[a].PilotName.PadRight(15) } - " +
                        $"{pilots[a].Laps} Laps in {pilots[a].Total} - " +
                        $"Best Lap was {pilots[a].BestLap} with {pilots[a].BestTime} - " +
                        $"Average Speed was {pilots[a].AverageSpeed.ToString("0.###")} - " +
                        $"Late on {pilots[a].Total - pilots[0].Total}");
                }

                var bestLapRace = pilots.OrderBy(o => o.BestTime).First();

                Console.WriteLine($"\nBest was in the " +
                    $"{bestLapRace.BestLap} Laps at " +
                    $"{bestLapRace.BestTime} performed by " +
                    $"{bestLapRace.PilotName.PadRight(15)}");

                Console.WriteLine("\nPress any key to exit");
                Console.ReadKey();
            }
        }

        public static string ExtractData(string line, int initialPosition, int dataLength)
        {
            if (string.IsNullOrEmpty(line))
                return string.Empty;

            if (initialPosition + dataLength > line.Length)
                return string.Empty;

            try
            {
                return line.Substring(initialPosition, dataLength).Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public class KartLap
        {
            public TimeSpan Time { get; set; }
            public string PilotCode { get; set; }
            public string PilotName { get; set; }
            public int LapNumber { get; set; }
            public TimeSpan LapTime { get; set; }
            public decimal LapAverageSpeed { get; set; }
        }
    }
}
