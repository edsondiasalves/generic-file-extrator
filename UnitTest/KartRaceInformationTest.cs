using Gympass_Interview;
using Gympass_Interview.Entities;
using Gympass_Interview.Extractors;
using Gympass_Interview.Helpers;
using Gympass_Interview.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class KartRaceInformationTest
    {
        [TestMethod]
        public void RaceInformationTest()
        {
            //Arrange
            var extractService = new ExtractDataFake();
            var computeService = new ComputeRankingKartRaceService();
            var outputService = new OutputKartRaceInformationService();

            //Act
            KartRaceInformation raceInfo = new KartRaceInformation(extractService, computeService, outputService);
            raceInfo.ExtractDataFromFile("bazinga.txt");
            raceInfo.ComputeRanking();
            raceInfo.GetBestLap();
            raceInfo.PrintOutput();

            //Assert
            Assert.AreEqual(raceInfo.Laps.Count, 4);
            Assert.AreEqual(raceInfo.Ranking.Count, 2);

            Assert.AreEqual(raceInfo.Ranking[0].PilotCode, "001");
            Assert.AreEqual(raceInfo.Ranking[0].PilotName, "Pilot 1");
            Assert.AreEqual(raceInfo.Ranking[0].BestLapNumber, 2);

            Assert.AreEqual(raceInfo.Ranking[1].PilotCode, "002");
            Assert.AreEqual(raceInfo.Ranking[1].PilotName, "Pilot 2");
            Assert.AreEqual(raceInfo.Ranking[1].BestLapNumber, 1);

            Assert.AreEqual(raceInfo.BestLap.PilotCode, "001");
            Assert.AreEqual(raceInfo.BestLap.PilotName, "Pilot 1");
            Assert.AreEqual(raceInfo.BestLap.BestLapNumber, 2);
        }

        public class ExtractDataFake : IExtractKartRaceFileService
        {
            public List<KartLap> Laps { get; set; }
            public List<KartLap> ExtractDataFromFile(string filePath)
            {
                List<KartLap> kartLaps = new List<KartLap>();
                kartLaps.Add(new KartLap()
                {
                    PilotCode = "001",
                    PilotName = "Pilot 1",
                    LapNumber = 1,
                    Time = new TimeSpan(0, 1, 1, 1, 1234),
                    LapTime = new TimeSpan(0, 1, 1, 1, 1234),
                    LapAverageSpeed = 10
                });

                kartLaps.Add(new KartLap()
                {
                    PilotCode = "002",
                    PilotName = "Pilot 2",
                    LapNumber = 1,
                    Time = new TimeSpan(0, 1, 1, 1, 1222),
                    LapTime = new TimeSpan(0, 1, 1, 1, 1222),
                    LapAverageSpeed = 10
                });

                kartLaps.Add(new KartLap()
                {
                    PilotCode = "001",
                    PilotName = "Pilot 1",
                    LapNumber = 2,
                    Time = new TimeSpan(0, 1, 1, 1, 1220),
                    LapTime = new TimeSpan(0, 1, 1, 1, 1220),
                    LapAverageSpeed = 10
                });

                kartLaps.Add(new KartLap()
                {
                    PilotCode = "002",
                    PilotName = "Pilot 2",
                    LapNumber = 2,
                    Time = new TimeSpan(0, 1, 1, 1, 1233),
                    LapTime = new TimeSpan(0, 1, 1, 1, 1233),
                    LapAverageSpeed = 10
                });

                return kartLaps;
            }
        }
    }
}
