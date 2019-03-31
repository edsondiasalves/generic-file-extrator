using Gympass_Interview.Entities;
using System;
using System.Collections.Generic;

namespace Gympass_Interview.Services
{
    public class OutputKartRaceInformationService : IOutputKartRaceInformationService
    {
        public void PrintLapsLog(List<KartLap> laps)
        {
            if (laps == null || laps.Count == 0)
            {
                Console.WriteLine("\nThere is not lap information to show");
                return;
            }

            Console.WriteLine(
                $"{"\nTime".PadRight(21) }" +
                $"{"Code".PadRight(6) }" +
                $"{"Name".PadRight(15) }" +
                $"{"Lap".PadRight(5) }" +
                $"{"Time".PadRight(20) }" +
                $"{"Average Speed"}"
            );

            foreach (KartLap kartLap in laps)
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

        public void PrintRanking(List<KartRaceResult> ranking)
        {
            if (ranking == null || ranking.Count == 0)
            {
                Console.WriteLine("\nThere is not ranking information to show");
                return;
            }

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

            for (var a = 0; a < ranking.Count; a++)
            {
                Console.WriteLine(
                    $"{a + 1 }º {"".PadRight(7) } " +
                    $"{ranking[a].PilotName.PadRight(15) }" +
                    $"{ranking[a].Lap.ToString().PadRight(8) }" +
                    $"{ranking[a].TotalTime.ToString().PadRight(19) }" +
                    $"{ranking[a].BestLapNumber.ToString().PadRight(11) }" +
                    $"{ranking[a].BestLapTime.ToString().PadRight(19) }" +
                    $"{ranking[a].AverageSpeed.ToString("0.###").PadRight(19) }" +
                    $"{ranking[a].TotalTime - ranking[0].TotalTime }");
            }
        }

        public void PrintBestLapInfo(KartRaceResult bestLap)
        {
            if (bestLap == null || bestLap.Lap == 0)
            {
                Console.WriteLine("\nThere is not best lap information to show");
                return;
            }

            Console.WriteLine($"\nThe " +
                $"{bestLap.BestLapNumber} lap of " +
                $"{bestLap.PilotName} was the fastest lap with " +
                $"{bestLap.BestLapTime} ");
        }
    }
}
