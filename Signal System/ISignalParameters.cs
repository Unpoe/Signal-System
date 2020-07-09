namespace Signals
{
    public interface ISignalParameters
    {
        void AddParameter(string key, object value);
        object GetParameter(string key);
        bool HasParameter(string key);
    }
}

