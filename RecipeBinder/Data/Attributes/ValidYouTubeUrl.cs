using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RecipeBinder.Data.Attributes
{
    public class ValidYouTubeUrl : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string url = (string)value;
            string[] starts = { "https://youtube.com", "https://youtu.be" };
            if (string.IsNullOrWhiteSpace(url) || starts.Any(s => url.StartsWith(s))) return ValidationResult.Success;
            return new ValidationResult($"Invalid URL; YouTube Video URLs must begin with {starts[0]} or {starts[1]}", new[] { validationContext.MemberName });
        }
    }
}
