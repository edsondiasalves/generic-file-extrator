using System.Collections.Generic;
using Gympass_Interview.Entities;

namespace Gympass_Interview.Services
{
    public interface IComputeRankingKartRaceService
    {
        List<KartRaceResult> ComputeRanking(List<KartLap> kartLaps);
        KartRaceResult GetBestLap();
    }
}