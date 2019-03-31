using System.Collections.Generic;
using Gympass_Interview.Entities;

namespace Gympass_Interview.Services
{
    public interface IOutputKartRaceInformationService
    {
        void PrintBestLapInfo(KartRaceResult bestLap);
        void PrintLapsLog(List<KartLap> laps);
        void PrintRanking(List<KartRaceResult> ranking);
    }
}