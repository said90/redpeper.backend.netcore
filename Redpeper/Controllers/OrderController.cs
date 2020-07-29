using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Redpeper.Dto;
using Redpeper.Hubs;
using Redpeper.Hubs.Clients;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Order;
using Redpeper.Repositories.Orders;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHubContext<OrderHub, IOrderClient> _orderHub;

        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;


        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IHubContext<OrderHub, IOrderClient> orderHub)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _orderHub = orderHub;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            return await _orderRepository.GetAll();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            return await _orderRepository.GetById(id);
        }

        [HttpGet("[action]/{orderNumber}")]
        public async Task<ActionResult<Order>> GetByOrderNumber(string orderNumber)
        {
            return await _orderRepository.GetByOrderNumber(orderNumber);
        }

        [HttpGet("[action]/{status}")]
        public async Task<ActionResult<List<Order>>> GetOrderByStatus(string status)
        {
            return await _orderRepository.GetOrdersByStatus(status);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto order)
        {
            try
            {
                var or = new Order
                {
                    CustomerId = order.CustomerId,
                    TableId = order.TableId,
                    Date = DateTime.Now,
                    Total = order.Total,
                    Status = "Abierta"
                };
                order.Date = DateTime.Now;
                order.Status = "Abierta";
                or.OrderNumber = "O-" + (await _orderRepository.GetOrderNumber()+1);
                _orderRepository.Create(or);
                await _unitOfWork.Commit();

                var details = order.OrderDetails.Select(x => new OrderDetail
                {
                    DishId = x.DishId != 0 ? x.DishId : null,
                    ComboId = x.ComboId != 0 ? x.ComboId : null,
                    Discount = x.Discount,
                    Qty = x.Qty,
                    OrderId = or.Id,
                    Status = "En Cola",
                    Total = x.Total,
                    Comments = x.Comments,
                    UnitPrice = x.UnitPrice
                }).ToList();

               
                _orderDetailRepository.CreateRange(details);

                await _unitOfWork.Commit();

                order.OrderDetails = details;
                order.Id = or.Id;
                order.OrderNumber = or.OrderNumber;
                await _orderHub.Clients.All.ReceiveMessage(order);

                return order;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }

        }
    }
}
