using System.ComponentModel.DataAnnotations.Schema;

namespace GallaryStore.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public double rate { get; set; }
        public string? img { get; set; }

        [ForeignKey("category")]
        public int categoryID { get; set; }
        public Category category { get; set; }

        public List<OrderProducts> Orderproducts { get; set; } 
        public List<Favourite> favourites { get; set; }
    }
}
