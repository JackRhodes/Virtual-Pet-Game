using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using virtual_pet_game.Areas.v1.Models.Mappings;

namespace virtual_pet_game.Tests.v1.Models.Helper
{
    public static class HelperMethods
    {
        /// <summary>
        /// Checks Model to see if it passes Validation
        /// </summary>
        /// <param name="model">Model to check.</param>
        /// <returns>Valid (true) or invalid (false).</returns>
        public static bool CheckModelValidation(object model)
        {
            //Create a context for the validation.
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model, null, null);
            //Create a storage method to store results of the check.
            var result = new List<ValidationResult>();
            //Check whether the Model provided is valid against the context.
            bool valid = Validator.TryValidateObject(model, validationContext, result, true);

            return valid;
        }

        public static string GenerateLargeString(int maxValue)
        {

            StringBuilder stringBuilder = new StringBuilder();

            string characters = "abcdefghijklmnopqrstuvwxyz";

            Random random = new Random();

            for (int i = 0; i < maxValue; i++)
            {
                stringBuilder.Append(characters[random.Next(characters.Length - 1)]);
            }

            return stringBuilder.ToString();
        }

        public static void InitialiseAutoMapper()
        {
            Mapper.Reset();

            //As automapper is static, it can be initalised in here to replicate the functionality offered by Startup.cs
            Mapper.Initialize(x =>
            {
                x.AddProfile(new DTOMappings());
            });
        }
    }
}
