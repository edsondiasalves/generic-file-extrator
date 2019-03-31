using System;

namespace Gympass_Interview.Helpers
{
    public class DataExtractedParser : IDataExtractedParser
    {
        public DataExtractedParser() { }

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
}
