using PSP.Domain.Exceptions;
using PSP.Domain.Validators.UserValidators;

namespace PSP.Domain.Aggregates.UserAggregate
{

    public class BasicInfo
    {

        private BasicInfo()
        {
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public string EmailAddress { get; private set; }

        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string userName, string email)
        {
            var validator = new BasicInfoValidator();

            var objToValidate = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                EmailAddress = email
            };

            var validationResult = validator.Validate(objToValidate);

            if (validationResult.IsValid) return objToValidate;

            var exception = new UserProfileNotValidException("The user profile is not valid");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add(error.ErrorMessage);
            }

            throw exception;
        }
    }
}