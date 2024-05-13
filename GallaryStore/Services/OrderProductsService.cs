using GallaryStore.DTOs;
using GallaryStore.DTOs.orderProducts;
using GallaryStore.Models;
using GallaryStore.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace GallaryStore.Services
{
    public class OrderProductsService
    {
        public unitOfWork<OrderProducts> unit;
        public unitOfWork<Product> unitProduct;
        public OrderProductsService(unitOfWork<OrderProducts> unit, unitOfWork<Product> unitProduct)
        {
            this.unit = unit;
            this.unitProduct = unitProduct;
        }

        public List<getOrderProductsDTO> GetAll()
        {
            return unit.Repository.GetAll().Select(p => new getOrderProductsDTO(p.orderId,p.productId, p.quantity,p.subtotal)).ToList();
        }
        public List<getOrderProductsDTO> GetById(int orderId,string include)
        {

            return unit.Repository.getElements(p => p.orderId == orderId, include).Select(p => new getOrderProductsDTO(p.orderId, p.productId, p.quantity, p.subtotal)).ToList();
            
        }

        public void Update(OrderProductsDTO OrderProducts)
        {
            Product product = unitProduct.Repository.GetById(OrderProducts.productId);

            OrderProducts p = new OrderProducts()
            {
                orderId= OrderProducts.orderId,
                productId= OrderProducts.productId,
                quantity= OrderProducts.quantity,
                subtotal= product.price * OrderProducts.quantity,
                
                

            };
            unit.Repository.update(p);
            unit.savechanges();
        }
        public void Delete(getOrderProductsDTO orderProducts)
        {
            OrderProducts OrderProducts = unit.Repository.getElement(p => p.productId == orderProducts.productId && p.orderId == orderProducts.orderId,null);
            unit.Repository.delete(OrderProducts);
            unit.savechanges();
        }
        public void Add(OrderProductsDTO OrderProducts)
        {
            Product product = unitProduct.Repository.GetById(OrderProducts.productId);
            OrderProducts p = new OrderProducts()
            {
                orderId = OrderProducts.orderId,
                productId = OrderProducts.productId,
                quantity = OrderProducts.quantity,
                subtotal = product.price * OrderProducts.quantity,
                
            };
            unit.Repository.add(p);
            unit.savechanges();
        }
    }
}
