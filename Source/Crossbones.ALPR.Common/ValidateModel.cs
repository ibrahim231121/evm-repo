using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Common
{
    /// <summary>
    /// To validate model either it meets all the requirement to be proceed futher or not
    /// </summary>
    /// <typeparam name="T">Model to be validate</typeparam>
    public class ValidateModel<T>
    {
        /// <summary>
        /// Takes model to validate
        /// </summary>
        /// <param name="model">Model to be validate</param>
        /// <returns>bool(whether model validates or not) and string(error message in string with ',' separator)</returns>
        public (bool, string) Validate(T model)
        {
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
            var errorMessages = results.Select(x => x.ErrorMessage);
            return (valid, string.Join(" ", errorMessages));
        }
    }
}