namespace GallaryStore.DTOs.order
{
    public class AddOrderDTO
    {
        public AddOrderDTO(DateTime? checkOutDate,int? quantity, char status)
        {

            
            this.checkOutDate = checkOutDate;
            this.quantity = quantity;
            this.status = status;
            //this.userId = userId;

        }
       
        public DateTime? checkOutDate { get; set; }
        public int? quantity { get; set; }
        public char status { get; set; }
        //public string userId { get; set; }
    }
}
