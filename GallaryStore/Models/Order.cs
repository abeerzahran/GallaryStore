using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GallaryStore.Models
{
    public class Order
    {
        public int id { get; set; }
        public DateTime? checkOutDate { get; set; }
        public decimal? totalPrice { get; set; }
        public int? quantity { get; set; }
        public char status { get; set; }

        [ForeignKey("user")]
        public string userId { get; set; }

        public ApplicationUser user { get; set; }

        public List<OrderProducts> OrderProducts { get; set; }
    }
}
