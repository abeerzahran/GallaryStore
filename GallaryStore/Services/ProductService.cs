﻿using GallaryStore.DTOs.product;
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
            Product p = new Product()
            {
                id= product.id,
                name= product.name,
                description= product.description,
                price= product.price,
                quantity= product.quantity,
                rate= product.rate,
                img=(product.img!=null)?product.img : product1.img ,
                categoryID= product.categoryID,
              
            };
            unit.Repository.update(p);
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
        
    }
}
