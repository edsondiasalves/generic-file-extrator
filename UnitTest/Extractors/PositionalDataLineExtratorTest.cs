using Gympass_Interview.Extractors;
using Gympass_Interview.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest.Extractors
{
    [TestClass]
    public class PositionalDataLineExtratorTest
    {
        [TestMethod]
        public void ExtractSimpleInformation()
        {
            //Arrange
            DataExtractedParser dep = new DataExtractedParser();
            PositionalDataLineExtrator<Test> pdle = new PositionalDataLineExtrator<Test>(dep);

            //Act
            Test testClass = pdle.Extract("1234523:49:08.277ABCDE22,50");

            //Assert
            Assert.AreEqual(testClass.MyNumber, 12345);
            Assert.AreEqual(testClass.MyTime, TimeSpan.Parse("23:49:08.277"));
            Assert.AreEqual(testClass.MyText, "ABCDE");
            Assert.AreEqual(testClass.MyDecimal, Decimal.Parse("22,50"));
        }

        public class Test {
            [PositionalDataExtract(0, 5)]
            public int MyNumber { get; set; }
            [PositionalDataExtract(5, 12)]
            public TimeSpan MyTime { get; set; }
            [PositionalDataExtract(17, 5)]
            public string MyText { get; set; }
            [PositionalDataExtract(22, 5)]
            public decimal MyDecimal { get; set; }
        }
    }
}
