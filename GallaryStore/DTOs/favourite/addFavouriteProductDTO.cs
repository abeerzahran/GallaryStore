using GallaryStore.DTOs.product;

namespace GallaryStore.DTOs.favourite
{
    public class addFavouriteProductDTO
    {
        public addFavouriteProductDTO( int productId)
        {

            this.productId = productId;
        }
        public int productId { get; set; }

    }
}
