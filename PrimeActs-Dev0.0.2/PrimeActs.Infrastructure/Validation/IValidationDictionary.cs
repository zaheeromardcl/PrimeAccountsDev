namespace PrimeActs.Infrastructure.Validation
{
    public interface IValidationDictionary
    {
        bool IsValid { get; }
        void AddError(string key, string errorMessage);
    }
}