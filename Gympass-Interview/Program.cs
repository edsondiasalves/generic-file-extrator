using Gympass_Interview.Entities;
using Gympass_Interview.Extrators;
using Gympass_Interview.Helpers;
using Gympass_Interview.Services;
using System;

namespace Gympass_Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataParser = new DataExtractedParser();
            var positionalExtractorService = new PositionalDataLineExtrator<KartLap>(dataParser);

            var extractService = new ExtractKartRaceFileService(positionalExtractorService);
            var computeService = new ComputeRankingKartRaceService();
            var outputService = new OutputKartRaceInformationService();

            KartRaceInformation raceInfo = new KartRaceInformation(extractService, computeService, outputService);

            raceInfo.ExtractDataFromFile("data.txt");
            raceInfo.ComputeRanking();
            raceInfo.GetBestLap();
            raceInfo.PrintOutput();

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
