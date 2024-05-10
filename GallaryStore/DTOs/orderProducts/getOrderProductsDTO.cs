namespace GallaryStore.DTOs.orderProducts
{
    public class getOrderProductsDTO
    {
        public getOrderProductsDTO()
        {

        }
        public getOrderProductsDTO(int orderId, int productId, int? quantity , decimal? subTotal)
        {
            this.orderId = orderId;
            this.productId = productId;
            this.quantity = quantity;
            this.subTotal = subTotal;


        }

        public int orderId { get; set; }
        public int productId { get; set; }
        public int? quantity { get; set; }
        public decimal? subTotal { get; set; }

    }
}
