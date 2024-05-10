using GallaryStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GallaryStore.DTOs
{
    public class FavouriteDTO
    {
        public FavouriteDTO()
        {
            
        }
        public FavouriteDTO(string userId, int productId)
        {
            
            this.productId = productId;
            this.userId = userId;

        }
        
        public string userId { get; set; }
        public int productId { get; set; }

      
    }
}
