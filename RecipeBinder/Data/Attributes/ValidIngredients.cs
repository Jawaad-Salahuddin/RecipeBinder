using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Castle.Core.Internal;
using RecipeBinder.Data.Models;

namespace RecipeBinder.Data.Attributes
{
    public class ValidIngredients : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<Ingredient> ingredients = (List<Ingredient>)value;
            ingredients.RemoveAll(i => string.IsNullOrWhiteSpace(i.Name));
            if (ingredients.IsNullOrEmpty())
                return new ValidationResult($"Ingredients are required.", new[] { validationContext.MemberName });
            return ValidationResult.Success;
        }
    }
}
