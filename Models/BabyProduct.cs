namespace BabySphere.Models
{
    public class BabyProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public double Rating { get; set; }
    }
}
