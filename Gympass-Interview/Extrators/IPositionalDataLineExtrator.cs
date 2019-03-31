namespace Gympass_Interview.Extrators
{
    public interface IPositionalDataLineExtrator<T> where T : new()
    {
        T Extract(string line);
    }
}