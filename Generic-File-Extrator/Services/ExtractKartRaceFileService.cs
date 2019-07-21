using Gympass_Interview.Entities;
using Gympass_Interview.Extractors;
using Gympass_Interview.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gympass_Interview.Services
{
    public class ExtractKartRaceFileService : IExtractKartRaceFileService
    {
        public List<KartLap> Laps { get; set; }

        private IPositionalDataLineExtrator<KartLap> positionalExtractor;
        private PositionalDataLineExtrator<KartLap> positionalExtractorService;

        public ExtractKartRaceFileService(IPositionalDataLineExtrator<KartLap> positionalExtractor)
        {
            this.positionalExtractor = positionalExtractor;
            this.Laps = new List<KartLap>();
        }

        public List<KartLap> ExtractDataFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return this.Laps;
            }

            using (var sr = File.OpenText(filePath))
            {
                while (!sr.EndOfStream)
                {
                    KartLap lap = positionalExtractor.Extract(sr.ReadLine());
                    if (!string.IsNullOrEmpty(lap.PilotCode))
                    {
                        this.Laps.Add(lap);
                    }
                }
            }

            return this.Laps;
        }
    }
}
