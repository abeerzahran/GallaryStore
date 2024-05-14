using GallaryStore.DTOs.orderProducts;
using GallaryStore.Models;

namespace GallaryStore.DTOs.order
{
    public class CartDTO
    {
        public CartDTO(int id,DateTime? checkOutDate, decimal? totalPrice, int? quantity, char status, string userId, List<getOrderProductsDTO>products)
        {

            this.id = id;
            this.checkOutDate = checkOutDate;
            this.totalPrice = totalPrice;
            this.quantity = quantity;
            this.status = status;
            this.userId = userId;
            this.orderProducts = products;

        }

        public int id {  get; set; }
        public DateTime? checkOutDate { get; set; }
        public decimal? totalPrice { get; set; }
        public int? quantity { get; set; }
        public char status { get; set; }
        public string userId { get; set; }
        public List<getOrderProductsDTO> orderProducts { get; set; }
    }
}
