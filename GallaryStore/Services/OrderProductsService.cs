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
        public unitOfWork<Order> unitOrder;
        public OrderProductsService(unitOfWork<OrderProducts> unit, unitOfWork<Product> unitProduct, unitOfWork<Order> unitOrder)
        {
            this.unit = unit;
            this.unitProduct = unitProduct;
            this.unitOrder = unitOrder;
        }

        public List<getOrderProductsDTO> GetAll()
        {
            return unit.Repository.GetAll().Select(p => new getOrderProductsDTO(p.orderId,p.productId, p.quantity,p.subtotal)).ToList();
        }
        public List<getOrderProductsDTO> GetById(int orderId,string include)
        {

            return unit.Repository.getElements(p => p.orderId == orderId, include).Select(p => new getOrderProductsDTO(p.orderId, p.productId, p.quantity, p.subtotal)).ToList();
            
        }

        public getOrderProductsDTO Update(OrderProductsDTO OrderProducts)
        {
            Product product = unitProduct.Repository.GetById(OrderProducts.productId);

            OrderProducts p = new OrderProducts()
            {
                orderId= OrderProducts.orderId,
                productId= OrderProducts.productId,
                quantity= OrderProducts.quantity,
                subtotal= product.price * OrderProducts.quantity
            };
            getOrderProductsDTO orderProduct = new getOrderProductsDTO()
            {
                orderId = p.orderId,
                productId = p.productId,
                quantity = p.quantity,
                subTotal=p.subtotal
            };
            unit.Repository.update(p);
            unit.savechanges();
            return orderProduct;
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

        public getOrderProductsDTO? getProductInCart(int productId, string userId)
        {
            Order? cart = unitOrder.Repository.getElement(c => c.userId == userId && c.status == 'c', null);
            OrderProducts? orderProduct= unit.Repository.getElement(p=>p.orderId== cart.id&& p.productId==productId, null);
            if(orderProduct == null || cart==null)
            {
                return null;
            }
            return new getOrderProductsDTO(orderProduct.orderId, orderProduct.productId, orderProduct.quantity, orderProduct.subtotal);

        }
    }
}
