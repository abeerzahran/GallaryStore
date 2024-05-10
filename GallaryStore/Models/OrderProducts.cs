using System.ComponentModel.DataAnnotations.Schema;

namespace GallaryStore.Models
{
    public class OrderProducts
    {
        [ForeignKey("order")]
        public int orderId { get; set; }

        [ForeignKey("product")]
        public int productId { get; set; }
        public int? quantity { get; set; }
        public decimal? subtotal { get; set; }

        public Order order { get; set; }
        public Product product { get; set; }
    }
}
