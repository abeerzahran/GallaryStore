using GallaryStore.DTOs.product;

namespace GallaryStore.DTOs.favourite
{
    public class userFavoritsDTO
    {
        public userFavoritsDTO()
        {

        }
        public userFavoritsDTO(string userId, int productId)
        {

            this.productId = productId;
            this.userId = userId;


        }

        public string userId { get; set; }
        public int productId { get; set; }
        public ProductDTO  product { get; set; }

    }
}
