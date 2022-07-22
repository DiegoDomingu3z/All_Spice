using System;

namespace All_Spice.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime updatedAt { get; set; }

        public string Picture { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string category { get; set; }

        public string CreatorId { get; set; }

        public Profile Creator { get; set; }


    }
}