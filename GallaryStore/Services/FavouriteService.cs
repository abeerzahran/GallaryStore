using GallaryStore.DTOs;
using GallaryStore.DTOs.product;
using GallaryStore.Models;
using GallaryStore.UnitOfWork;

namespace GallaryStore.Services
{
    public class FavouriteService
    {
        public unitOfWork<Favourite> unit;
        public FavouriteService(unitOfWork<Favourite> unit)
        {
            this.unit = unit;
        }

        public List<FavouriteDTO> GetAll()
        {
            return unit.Repository.GetAll().Select(p => new FavouriteDTO( p.userId, p.productId)).ToList();
        }
        public FavouriteDTO GetById(int prodId, string userId,string include)
        {

            Favourite? p = unit.Repository.getElement(p=>p.productId==prodId && p.userId==userId,include);
            if (p == null)
                return null;
            return new FavouriteDTO(p.userId,p.productId);
        }

        public List<ProductDTO> GetByUserId(string userId, string include)
        {
            return unit.Repository.getElements(p => p.userId == userId, include).Select(p => new ProductDTO(p.product.id, p.product.name, p.product.description, p.product.price, p.product.quantity, p.product.rate, p.product.img, p.product.categoryID)).ToList();
        }

        public void Update(FavouriteDTO Favourite)
        {
            Favourite p = new Favourite()
            {
               
                userId = Favourite.userId,
                productId = Favourite.productId,
            };
            unit.Repository.update(p);
            unit.savechanges();
        }
        public void Delete(FavouriteDTO favourite)
        {
            Favourite Favourite = unit.Repository.getElement(p => p.productId == favourite.productId && p.userId == favourite.userId, null);
            unit.Repository.delete(Favourite);
            unit.savechanges();
        }
        public void Add(FavouriteDTO Favourite)
        {
            Favourite p = new Favourite()
            {
                
                userId = Favourite.userId,
                productId = Favourite.productId,
                
            };
            unit.Repository.add(p);
            unit.savechanges();
        }

        
    }
}
