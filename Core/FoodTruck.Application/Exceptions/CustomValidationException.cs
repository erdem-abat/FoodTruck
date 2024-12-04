using FoodTruck.Application.Errors;

namespace FoodTruck.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(List<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public List<ValidationError> ValidationErrors { get; set; }
    }
}
