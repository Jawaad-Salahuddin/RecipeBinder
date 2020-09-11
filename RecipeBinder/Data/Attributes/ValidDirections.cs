using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Castle.Core.Internal;
using RecipeBinder.Data.Models;

namespace RecipeBinder.Data.Attributes
{
    public class ValidDirections : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<Direction> directions = (List<Direction>)value;
            directions.RemoveAll(d => string.IsNullOrWhiteSpace(d.Description));
            if (directions.IsNullOrEmpty())
                return new ValidationResult($"Directions are required.", new[] { validationContext.MemberName });
            if (directions.Any(d => d.Step > directions.Count()) || directions.Select(d => d.Step).Distinct().Count() != directions.Count())
                return new ValidationResult($"Duplicate steps are not allowed.", new[] { validationContext.MemberName });
            return ValidationResult.Success;
        }
    }
}
