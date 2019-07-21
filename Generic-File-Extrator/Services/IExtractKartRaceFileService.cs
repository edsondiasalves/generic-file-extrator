using System.Collections.Generic;
using Gympass_Interview.Entities;

namespace Gympass_Interview.Services
{
    public interface IExtractKartRaceFileService
    {
        List<KartLap> Laps { get; set; }
        List<KartLap> ExtractDataFromFile(string filePath);
    }
}