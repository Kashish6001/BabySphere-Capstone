using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class BabyProduct
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Range(0, 10000)]
        public int Quantity { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }
    }
}

//namespace BabySphere.Models
//{
//    public class BabyProduct
//    {
//        public int Id { get; set; }

//        public string Name { get; set; }

//        public string Category { get; set; }

//        public decimal Price { get; set; }

//        public string Description { get; set; }

//        public string ImageUrl { get; set; }

//        public int Quantity { get; set; }

//        public double Rating { get; set; }
//    }
//}
