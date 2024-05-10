using GallaryStore.DTOs;
using GallaryStore.Models;
using GallaryStore.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace GallaryStore.Services
{
    public class OrderProductsService
    {
        public unitOfWork<OrderProducts> unit;
        public OrderProductsService(unitOfWork<OrderProducts> unit)
        {
            this.unit = unit;
        }

        public List<OrderProductsDTO> GetAll()
        {
            return unit.Repository.GetAll().Select(p => new OrderProductsDTO(p.orderId,p.productId, p.quantity,p.subtotal)).ToList();
        }
        public List<OrderProductsDTO> GetById(int orderId,string include)
        {

            return unit.Repository.getElements(p => p.orderId == orderId, include).Select(p => new OrderProductsDTO(p.orderId, p.productId, p.quantity, p.subtotal)).ToList();
            
        }

        public void Update(OrderProductsDTO OrderProducts)
        {
            OrderProducts p = new OrderProducts()
            {
                orderId= OrderProducts.orderId,
                productId= OrderProducts.productId,
                quantity= OrderProducts.quantity,
                subtotal= OrderProducts.subtotal,
                
                

            };
            unit.Repository.update(p);
            unit.savechanges();
        }
        public void Delete(OrderProductsDTO orderProducts)
        {
            OrderProducts OrderProducts = unit.Repository.getElement(p => p.productId == orderProducts.productId && p.orderId == orderProducts.orderId,null);
            unit.Repository.delete(OrderProducts);
            unit.savechanges();
        }
        public void Add(OrderProductsDTO OrderProducts)
        {
            OrderProducts p = new OrderProducts()
            {
                orderId = OrderProducts.orderId,
                productId = OrderProducts.productId,
                quantity = OrderProducts.quantity,
                subtotal = OrderProducts.subtotal,
                
            };
            unit.Repository.add(p);
            unit.savechanges();
        }
    }
}
