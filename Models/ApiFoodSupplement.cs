namespace Naringskollen.Models
{
    public class ApiFoodSupplement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LivsmedelverketId { get; set; }

        public double? Oxalate { get; set; }

        public double? GramPerDl { get; set; }

        public double? GramPerPiece { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
