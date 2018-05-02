namespace PrimeActs.Infrastructure.Localization
{
    public interface ILocalizer<T>
    {
        T GetValue(string key);
        void AddValue(string key, T value);
    }
}