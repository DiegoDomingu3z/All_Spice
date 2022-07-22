namespace All_Spice.Models
{
    public class Step
    {
        public int Id { get; set; }

        public int stepPosition { get; set; }

        public string body { get; set; }

        public int recipeId { get; set; }

        public Profile Creator { get; set; }

    }
}