using GallaryStore.DTOs.product;
using GallaryStore.Models;
using GallaryStore.UnitOfWork;

namespace GallaryStore.Services
{
    public class ProductService
    {
        public unitOfWork<Product> unit;
        public ProductService(unitOfWork<Product> unit)
        {
            this.unit = unit;
        }

        public List<ProductDTO> GetAll()
        {
            return unit.Repository.GetAll().Select(p => new ProductDTO(p.id, p.name, p.description, p.price, p.quantity, p.rate,p.img, p.categoryID)).ToList();
        }
        public ProductDTO GetById(int? id)
        {
            
            Product? p= unit.Repository.GetById(id);
            if (p == null)
                return null;
            return new ProductDTO(p.id, p.name, p.description, p.price, p.quantity, p.rate,p.img, p.categoryID);
        }
        
        public void Update(ProductDTO product)
        {
            Product product1 = unit.Repository.GetById(product.id);
            
            product1.name = product.name;
            product1.description = product.description;
            product1.price = product.price;
            product1.quantity = product.quantity;
            product1.rate = product.rate;
            product1.img = (product.img != null) ? product.img : product1.img;
            product1.categoryID = product.categoryID;

            
            unit.Repository.update(product1);
            unit.savechanges();
        }
        public void Delete(int id)
        {
            Product product= unit.Repository.GetById(id);
            unit.Repository.delete(product);
            unit.savechanges();
        }
        public void Add(AddProductDTO product)
        {
            Product p = new Product()
            {
                id = 0,
                name = product.name,
                description = product.description,
                price = product.price,
                quantity = product.quantity,
                rate = product.rate,
                img= product.img,
                categoryID = product.categoryID,
  
            };
            unit.Repository.add(p);
            unit.savechanges();
        }

        public List<ProductDTO> getProductPage(string searchTerm,int pageNum  ,int pageSize)
        {
            var products = unit.Repository.getElements(p => p.name == null ? " ".Contains(searchTerm) : p.name.Contains(searchTerm),null).ToList();
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            foreach (var product in products)
            {
                ProductDTO prodDTO = new ProductDTO()
                {
                    id= product.id,
                    name = product.name,
                    description = product.description,
                    price = product.price,
                    quantity = product.quantity,
                    rate = product.rate,
                    img= product.img,
                    categoryID = product.categoryID,

                };
                
                productsDTO.Add(prodDTO);
            }
            var totalCount = productsDTO.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            productsDTO = productsDTO.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            return productsDTO;
        }
        
    }
}
