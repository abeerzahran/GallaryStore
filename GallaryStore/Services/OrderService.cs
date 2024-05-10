using GallaryStore.DTOs;
using GallaryStore.DTOs.order;
using GallaryStore.Models;
using GallaryStore.UnitOfWork;

namespace GallaryStore.Services
{
    public class OrderService
    {
        public unitOfWork<Order> unit;
        public OrderService(unitOfWork<Order> unit)
        {
            this.unit = unit;
        }

        public List<OrderDTO> GetAll()
        {
            return unit.Repository.GetAll().Select(p => new OrderDTO(p.id,p.checkOutDate,p.totalPrice,p.quantity,p.status,p.userId)).ToList();
        }
        public OrderDTO GetById(int id)
        {

            Order p = unit.Repository.GetById(id);
            return new OrderDTO(p.id, p.checkOutDate, p.totalPrice, p.quantity, p.status, p.userId);
        }

        public void Update(OrderDTO Order)
        {
            Order p = new Order()
            {
                id = Order.id,
                checkOutDate = Order.checkOutDate,
                totalPrice = Order.totalPrice,
                quantity = Order.quantity,
                status = Order.status,
                userId = Order.userId,
               
                
            };
            unit.Repository.update(p);
            unit.savechanges();
        }
        public void Delete(int id)
        {
            Order Order = unit.Repository.GetById(id);
            unit.Repository.delete(Order);
            unit.savechanges();
        }
        public void Add(AddOrderDTO Order)
        {
            Order p = new Order()
            {
                id = 0,
                checkOutDate = Order.checkOutDate,
                totalPrice = Order.totalPrice,
                quantity = Order.quantity,
                status = Order.status,
                userId = Order.userId,
                
            };
            unit.Repository.add(p);
            unit.savechanges();
        }
    }
}
