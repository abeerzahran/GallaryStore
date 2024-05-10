using System.ComponentModel.DataAnnotations.Schema;

namespace GallaryStore.Models
{
    public class Favourite
    {
        [ForeignKey("user")]
        public string userId { get; set; }

        [ForeignKey("product")]
        public int productId { get; set; }

        public ApplicationUser user { get; set; }
        public Product product { get; set; }
    }
}
