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
        private ICustomerRepository _customerRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork, IHubContext<OrderHub> orderHub, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
            _orderHub = orderHub;
            _customerRepository = customerRepository;
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
            var customer = new Customer();
            if (order.CustomerId == null)
            {
                customer.Name = order.Name;
                customer.Lastname = order.LastName;
                
                _customerRepository.Create(customer);
                await _unitOfWork.Commit();
            }
            var or = new Order
            {
                CustomerId = customer.Id,
                Date = DateTime.Now,
                OrderNumber = order.OrderNumber,
                Status = "Abierta"
            };
            
            _orderRepository.Create(or);
            await _unitOfWork.Commit();

            var details = order.OrderDetails.Select(x => new OrderDetail
            {
                DishId = x.DishId,
                Discount = 0,
                Qty = x.Qty,
                OrderId = x.OrderId,
                Status = "En Cola",
                Total = x.Total,
                UnitPrice = x.UnitPrice
            }).ToList();
            _orderDetailRepository.CreateRange(details);
            await _unitOfWork.Commit();
            await _orderHub.Clients.All.SendAsync("ReceiveOrder", order);
            return order;
        }
    }
}
