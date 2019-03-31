using Gympass_Interview.Helpers;
using System.Reflection;

namespace Gympass_Interview.Extrators
{
    public class PositionalDataLineExtrator<T> : IPositionalDataLineExtrator<T> where T : new()
    {
        IDataExtractedParser dataParser;

        public PositionalDataLineExtrator(IDataExtractedParser parser)
        {
            dataParser = parser;
        }

        public T Extract(string line)
        {
            T obj = new T();
            PropertyInfo[] classProperties = typeof(T).GetProperties();

            foreach (PropertyInfo property in classProperties)
            {
                var attribute = property.GetCustomAttributes(typeof(PositionalDataExtractAttribute), true);

                if (attribute.Length > 0 && attribute[0] is PositionalDataExtractAttribute)
                {
                    PositionalDataExtractAttribute dea = (PositionalDataExtractAttribute)attribute[0];

                    if (dea.DataLenght == 0)
                    {
                        dea.DataLenght = line.Length - dea.InicialPosition;
                    }

                    if ((line.Length >= dea.InicialPosition + dea.DataLenght))
                    {
                        string data = line.Substring(dea.InicialPosition, dea.DataLenght);
                        object parse = null;

                        switch (property.PropertyType.FullName)
                        {
                            case "System.Int32":
                                parse = dataParser.ToInt32(data);
                                break;
                            case "System.TimeSpan":
                                parse = dataParser.ToTimeSpan(data);
                                break;
                            case "System.Decimal":
                                parse = dataParser.ToDecimal(data);
                                break;
                            default:
                                parse = data.ToString().Trim();
                                break;
                        }

                        property.SetValue(obj, parse);
                    }
                }
            }

            return obj;
        }
    }
}
