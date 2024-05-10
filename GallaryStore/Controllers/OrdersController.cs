﻿using GallaryStore.DTOs;
using GallaryStore.DTOs.order;
using GallaryStore.Models;
using GallaryStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GallaryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        OrderService orderService;
        public OrdersController(OrderService service)
        {
            orderService = service;
        }
        [HttpGet]
        public ActionResult getAll()
        {
            List<OrderDTO>? Orders = orderService.GetAll();

            if (Orders != null)
            {
                return Ok(Orders);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            OrderDTO? Order = orderService.GetById(id);
            if (Order != null)
            {
                return Ok(Order);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, OrderDTO Order)
        {
            if (ModelState.IsValid)
            {
                if (id == null || id == 0 || id != Order.id)
                {
                    return BadRequest();
                }
                try
                {
                    orderService.Update(Order);
                    return CreatedAtAction("getById", new { id = Order.id }, Order);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);

            
            
        }
        [HttpPost]
        public ActionResult Add(AddOrderDTO order)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    orderService.Add(order);
                    return Ok(order);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);

            
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            OrderDTO? Order = orderService.GetById(id);
            if (Order != null)
            {
                orderService.Delete(id);
                return Ok(Order);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
