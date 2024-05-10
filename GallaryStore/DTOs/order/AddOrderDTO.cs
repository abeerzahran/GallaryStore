namespace GallaryStore.DTOs.order
{
    public class AddOrderDTO
    {
        public AddOrderDTO(DateTime? checkOutDate, decimal? totalPrice, int? quantity, char status, string userId)
        {

            
            this.checkOutDate = checkOutDate;
            this.totalPrice = totalPrice;
            this.quantity = quantity;
            this.status = status;
            this.userId = userId;

        }
       
        public DateTime? checkOutDate { get; set; }
        public decimal? totalPrice { get; set; }
        public int? quantity { get; set; }
        public char status { get; set; }
        public string userId { get; set; }
    }
}
