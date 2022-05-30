using PSP.Domain.Exceptions;
using PSP.Domain.Validators.CategoryValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Domain.Aggregates.CategoryAggregate
{
    public class Category
    {
        private Category() { }

        public Guid CategoryId { get; private set; }
        
        public string CategoryName { get; private set; }
        public string CategoryDescription { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }


        public static Category CreateCategory (string categoryName, string categoryDescription)
        {
            var validator = new CategoryValidator();

            var objectToValidate = new Category
            {
                CategoryName = categoryName,
                CategoryDescription = categoryDescription,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objectToValidate);
            if (validationResult.IsValid) return objectToValidate;

            var exception = new CategoryNotValidException("Category is not valid.");
            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;
        }
    }
}
