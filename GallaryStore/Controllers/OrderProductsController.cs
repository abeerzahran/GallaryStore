using GallaryStore.DTOs;
using GallaryStore.DTOs.orderProducts;
using GallaryStore.Models;
using GallaryStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GallaryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        OrderProductsService OrderProductService;
        public OrderProductsController(OrderProductsService service)
        {
            OrderProductService = service;
        }
        [HttpGet]
        public ActionResult getAll()
        {
            List<getOrderProductsDTO>? OrderProducts = OrderProductService.GetAll();

            if (OrderProducts != null)
            {
                return Ok(OrderProducts);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{orderID}")]
        public ActionResult getById(int orderID)
        {
            if (orderID == null)
            {
                return BadRequest();
            }
            List<getOrderProductsDTO>? OrderProduct = OrderProductService.GetById(orderID, "product");
            if (OrderProduct != null)
            {
                return Ok(OrderProduct);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut]
        public ActionResult Update(OrderProductsDTO OrderProduct)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var orderProduct=OrderProductService.Update(OrderProduct);
                    return Ok(orderProduct);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);
           
        }
        [HttpPost]
        public ActionResult Add(OrderProductsDTO OrderProduct)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    OrderProductService.Add(OrderProduct);
                    return Ok(OrderProduct);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);

            
        }
        [HttpDelete]
        public ActionResult Delete(getOrderProductsDTO orderProducts)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    OrderProductService.Delete(orderProducts);
                    return Ok(orderProducts);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);
        }

        [HttpGet("getproduct/{id}")]
        [Authorize]
        public ActionResult getproduct(int id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            getOrderProductsDTO? product = OrderProductService.getProductInCart(id, userId);
            if (product == null)
                return Ok(null);
            return Ok(product);
        }
    }
}
