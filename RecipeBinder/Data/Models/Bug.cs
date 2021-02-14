using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBinder.Data.Models
{
    public class Bug
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual List<Image> Images { get; set; }
    }
}
