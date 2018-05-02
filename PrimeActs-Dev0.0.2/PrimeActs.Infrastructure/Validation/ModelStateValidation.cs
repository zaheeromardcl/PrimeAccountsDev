#region

using System.Web.ModelBinding;

#endregion

namespace PrimeActs.Infrastructure.Validation
{
    public class ModelStateValidation : IValidationDictionary
    {
        private readonly ModelStateDictionary modelState;

        public ModelStateValidation(ModelStateDictionary modelState)
        {
            this.modelState = modelState;
        }

        public void AddError(string key, string errorMessage)
        {
            modelState.AddModelError(key, errorMessage);
        }

        public bool IsValid
        {
            get { return modelState.IsValid; }
        }
    }
}