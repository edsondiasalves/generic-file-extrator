using System;

namespace Gympass_Interview.Helpers
{
    public interface IDataExtractedParser
    {
        decimal ToDecimal(string data);
        int ToInt32(string data);
        TimeSpan ToTimeSpan(string data);
    }
}