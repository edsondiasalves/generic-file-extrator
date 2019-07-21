using System;

namespace Gympass_Interview.Extractors
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PositionalDataExtractAttribute : Attribute
    {
        public int InicialPosition { get; set; }
        public int DataLenght { get; set; }
        public PositionalDataExtractAttribute(int initialPosition, int dataLenght)
        {
            this.InicialPosition = initialPosition;
            this.DataLenght = dataLenght;
        }
    }
}
