using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace virtual_pet_game.Tests.v1.Models.Helper
{
    public class ModelStateValidator
    {
        /// <summary>
        /// Checks Model to see if it passes Validation
        /// </summary>
        /// <param name="model">Model to check.</param>
        /// <returns>Valid (true) or invalid (false).</returns>
        public bool CheckModelValidation(object model)
        {
            //Create a context for the validation.
            ValidationContext validationContext = new ValidationContext(model, null, null);
            //Create a storage method to store results of the check.
            var result = new List<ValidationResult>();
            //Check whether the Model provided is valid against the context.
            bool valid = Validator.TryValidateObject(model, validationContext, result, true);

            return valid;
        }
    }
}
