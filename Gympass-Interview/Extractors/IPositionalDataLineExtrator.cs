namespace Gympass_Interview.Extractors
{
    public interface IPositionalDataLineExtrator<T> where T : new()
    {
        T Extract(string line);
    }
}