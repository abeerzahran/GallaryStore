using GallaryStore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GallaryStore.DTOs
{
    public class OrderDTO
    {
        public OrderDTO()
        { }
        public OrderDTO(int id, DateTime? checkOutDate, decimal? totalPrice, int? quantity, char status, string userId)
        {
        
            this.id = id;
            this.checkOutDate = checkOutDate;
            this.totalPrice = totalPrice;
            this.quantity = quantity;
            this.status = status;
            this.userId = userId;
            
        }
        public int id { get; set; }
        public DateTime? checkOutDate { get; set; }
        public decimal? totalPrice { get; set; }
        public int? quantity { get; set; }
        public char status { get; set; }
        public string userId { get; set; }

    }
}
