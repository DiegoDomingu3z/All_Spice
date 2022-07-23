namespace All_Spice.Models
{
    public class Step
    {
        public int Id { get; set; }

        public int? StepPosition { get; set; }

        public string Body { get; set; }

        public int RecipeId { get; set; }

        public Profile Creator { get; set; }

    }
}