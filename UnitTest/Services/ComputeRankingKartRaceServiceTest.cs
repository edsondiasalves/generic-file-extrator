using Gympass_Interview.Entities;
using Gympass_Interview.Extractors;
using Gympass_Interview.Helpers;
using Gympass_Interview.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest.Services
{
    [TestClass]
    public class KartRaceInformationTest
    {
        [TestMethod]
        public void RankingLapsByFirstPilot()
        {
            //Arrange
            ComputeRankingKartRaceService computeService = new ComputeRankingKartRaceService();

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

            //Act
            List<KartRaceResult> result = computeService.ComputeRanking(kartLaps);

            //Assert
            Assert.AreEqual(result.Count, 2);

            Assert.AreEqual(result[0].PilotCode, "001");
            Assert.AreEqual(result[0].PilotName, "Pilot 1");
            Assert.AreEqual(result[0].BestLapNumber, 2);

            Assert.AreEqual(result[1].PilotCode, "002");
            Assert.AreEqual(result[1].PilotName, "Pilot 2");
            Assert.AreEqual(result[1].BestLapNumber, 1);
        }

        [TestMethod]
        public void GetBestLapByFirstPilot()
        {
            //Arrange
            ComputeRankingKartRaceService computeService = new ComputeRankingKartRaceService();

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

            //Act
            computeService.ComputeRanking(kartLaps);
            KartRaceResult result = computeService.GetBestLap();

            //Assert
            Assert.AreEqual(result.PilotCode, "001");
            Assert.AreEqual(result.PilotName, "Pilot 1");
            Assert.AreEqual(result.BestLapNumber, 2);
        }

        [TestMethod]
        public void RankingLapsBySecondPilot()
        {
            //Arrange
            ComputeRankingKartRaceService computeService = new ComputeRankingKartRaceService();

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
                Time = new TimeSpan(0, 1, 1, 1, 1223),
                LapTime = new TimeSpan(0, 1, 1, 1, 1223),
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

            //Act
            List<KartRaceResult> result = computeService.ComputeRanking(kartLaps);

            //Assert
            Assert.AreEqual(result.Count, 2);

            Assert.AreEqual(result[0].PilotCode, "002");
            Assert.AreEqual(result[0].PilotName, "Pilot 2");
            Assert.AreEqual(result[0].BestLapNumber, 1);

            Assert.AreEqual(result[1].PilotCode, "001");
            Assert.AreEqual(result[1].PilotName, "Pilot 1");
            Assert.AreEqual(result[1].BestLapNumber, 2);
        }

        [TestMethod]
        public void GetBestLapBySecondPilot()
        {
            //Arrange
            ComputeRankingKartRaceService computeService = new ComputeRankingKartRaceService();

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
                Time = new TimeSpan(0, 1, 1, 1, 1223),
                LapTime = new TimeSpan(0, 1, 1, 1, 1223),
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

            //Act
            computeService.ComputeRanking(kartLaps);
            KartRaceResult result = computeService.GetBestLap();

            //Assert
            Assert.AreEqual(result.PilotCode, "002");
            Assert.AreEqual(result.PilotName, "Pilot 2");
            Assert.AreEqual(result.BestLapNumber, 1);
        }
    }
}
