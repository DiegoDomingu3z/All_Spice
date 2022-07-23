using System;

namespace All_Spice.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string AccountId { get; set; }

        public int RecipeId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Profile Creator { get; set; }

    }
}