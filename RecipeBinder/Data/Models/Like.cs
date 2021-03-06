﻿using System.ComponentModel.DataAnnotations;

namespace RecipeBinder.Data.Models
{
    public class Like
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
