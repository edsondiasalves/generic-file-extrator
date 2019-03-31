using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gympass_Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            KartRaceInformation raceInfo = new KartRaceInformation("data.txt");

            if (raceInfo.Laps.Count > 0)
            {
                raceInfo.PrintLapsLog();

                raceInfo.PrintRanking();

                raceInfo.PrintBestLapInfo();
            }
            else
            {
                Console.WriteLine("There is not kart race data to show");
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }

        public class KartLap
        {
            [DataExtract(0, 12)]
            public TimeSpan Time { get; set; }
            [DataExtract(18, 3)]
            public string PilotCode { get; set; }
            [DataExtract(24, 15)]
            public string PilotName { get; set; }
            [DataExtract(58, 1)]
            public int LapNumber { get; set; }
            [DataExtract(61, 12)]
            public TimeSpan LapTime { get; set; }
            [DataExtract(93, 0)]
            public decimal LapAverageSpeed { get; set; }
        }

        public class KartRaceInformation
        {
            public List<KartLap> Laps { get; set; }
            public List<KartRaceResult> Ranking { get; set; }
            public KartRaceResult BestLap { get; set; }

            public KartRaceInformation(string filePath)
            {
                this.Laps = new List<KartLap>();
                this.Ranking = new List<KartRaceResult>();

                this.ExtractDataFromFile(filePath);

                if (this.Laps.Count > 0)
                {
                    this.ComputeRanking();
                }
            }

            private List<KartLap> ExtractDataFromFile(string filePath)
            {
                using (var sr = File.OpenText(filePath))
                {
                    LineExtractor<KartLap> extrator = new LineExtractor<KartLap>();
                    while (!sr.EndOfStream)
                    {
                        KartLap lap = extrator.Extract(sr.ReadLine());
                        if (!string.IsNullOrEmpty(lap.PilotCode))
                        {
                            this.Laps.Add(lap);
                        }
                    }
                }

                return this.Laps;
            }

            private List<KartRaceResult> ComputeRanking()
            {
                if (this.Laps.Count < 1)
                {
                    return this.Ranking;
                }

                this.Ranking = (from lap in this.Laps
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

                this.BestLap = this.Ranking.OrderBy(o => o.BestLapTime).First();

                return this.Ranking;
            }

            public void PrintLapsLog()
            {
                Console.WriteLine(
                    $"{"\nTime".PadRight(21) }" +
                    $"{"Code".PadRight(6) }" +
                    $"{"Name".PadRight(15) }" +
                    $"{"Lap".PadRight(5) }" +
                    $"{"Time".PadRight(20) }" +
                    $"{"Average Speed"}"
                );

                foreach (KartLap kartLap in this.Laps)
                {
                    Console.WriteLine(
                        $"{kartLap.Time.ToString().PadRight(20) }" +
                        $"{kartLap.PilotCode.PadRight(6) }" +
                        $"{kartLap.PilotName.PadRight(15) }" +
                        $"{kartLap.LapNumber.ToString().PadRight(5) }" +
                        $"{kartLap.LapTime.ToString().PadRight(20) }" +
                        $"{kartLap.LapAverageSpeed.ToString("0.###")}"
                    );
                }
            }

            public void PrintRanking()
            {
                Console.WriteLine(
                    $"{"\nPosition".PadRight(12) }" +
                    $"{"Pilot".PadRight(15) }" +
                    $"{"Laps".PadRight(8) }" +
                    $"{"Time".PadRight(19) }" +
                    $"{"Best Lap".PadRight(11) }" +
                    $"{"Best Lap Time".PadRight(19) }" +
                    $"{"Average Speed".PadRight(19) }" +
                    $"{"Difference" }"
                );

                for (var a = 0; a < this.Ranking.Count; a++)
                {
                    Console.WriteLine(
                        $"{a + 1 }º {"".PadRight(7) } " +
                        $"{this.Ranking[a].PilotName.PadRight(15) }" +
                        $"{this.Ranking[a].Lap.ToString().PadRight(8) }" +
                        $"{this.Ranking[a].TotalTime.ToString().PadRight(19) }" +
                        $"{this.Ranking[a].BestLapNumber.ToString().PadRight(11) }" +
                        $"{this.Ranking[a].BestLapTime.ToString().PadRight(19) }" +
                        $"{this.Ranking[a].AverageSpeed.ToString("0.###").PadRight(19) }" +
                        $"{this.Ranking[a].TotalTime - this.Ranking[0].TotalTime }");
                }
            }

            public void PrintBestLapInfo()
            {
                Console.WriteLine($"\nThe " +
                    $"{this.BestLap.BestLapNumber} lap of " +
                    $"{this.BestLap.PilotName} was the fastest lap with " +
                    $"{this.BestLap.BestLapTime} ");
            }
        }

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

        public class LineExtractor<T> where T : new()
        {
            DataParser dataParser = new DataParser();

            public T Extract(string line)
            {
                T obj = new T();
                PropertyInfo[] classProperties = typeof(T).GetProperties();

                foreach (PropertyInfo property in classProperties)
                {
                    var attribute = property.GetCustomAttributes(typeof(DataExtractAttribute), true);

                    if (attribute.Length > 0 && attribute[0] is DataExtractAttribute)
                    {
                        DataExtractAttribute dea = (DataExtractAttribute)attribute[0];

                        if (dea.DataLenght == 0)
                        {
                            dea.DataLenght = line.Length - dea.InicialPosition;
                        }

                        if ((line.Length >= dea.InicialPosition + dea.DataLenght))
                        {
                            string data = line.Substring(dea.InicialPosition, dea.DataLenght);
                            object parse = null;

                            switch (property.PropertyType.FullName)
                            {
                                case "System.Int32":
                                    parse = dataParser.ToInt32(data);
                                    break;
                                case "System.TimeSpan":
                                    parse = dataParser.ToTimeSpan(data);
                                    break;
                                case "System.Decimal":
                                    parse = dataParser.ToDecimal(data);
                                    break;
                                default:
                                    parse = data.ToString().Trim();
                                    break;
                            }

                            property.SetValue(obj, parse);
                        }
                    }
                }

                return obj;
            }
        }

        public class DataParser
        {
            public DataParser() { }

            public TimeSpan ToTimeSpan(string data)
            {
                var split = data.Split(':');

                if (split.Length == 2)
                {
                    var minute = split[0];
                    var second = data.Split('.')[0].Split(':')[1];
                    var milisecond = data.Split('.')[1].Trim();

                    return new TimeSpan(0, 0, int.Parse(minute), int.Parse(second), int.Parse(milisecond));
                }

                TimeSpan time;
                TimeSpan.TryParse(data, out time);

                return time;
            }
            public Int32 ToInt32(string data)
            {
                Int32 number;
                Int32.TryParse(data, out number);

                return number;
            }

            public Decimal ToDecimal(string data)
            {
                Decimal number;
                Decimal.TryParse(data, out number);

                return number;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class DataExtractAttribute : Attribute
        {
            public int InicialPosition { get; set; }
            public int DataLenght { get; set; }
            public DataExtractAttribute(int initialPosition, int dataLenght)
            {
                this.InicialPosition = initialPosition;
                this.DataLenght = dataLenght;
            }
        }
    }
}
