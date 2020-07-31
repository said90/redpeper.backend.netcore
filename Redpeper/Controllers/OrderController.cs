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


        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,
            IUnitOfWork unitOfWork, ICustomerRepository customerRepository,
            IHubContext<OrderHub, IOrderClient> orderHub)
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
                or.OrderNumber = "O-" + (await _orderRepository.GetOrderNumber() + 1);
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
                await _orderHub.Clients.All.OrderCreated(order);
                return order;
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("[Action]/{id}")]
        public async Task<IActionResult> UpdateOrder([FromQuery]int id,List<OrderDetail> orderDetailsDto)
        {
            if (id !=0 && orderDetailsDto!=null)
            {
                var order = await _orderRepository.GetById(id);
                var orderDetailsToRemove = order.OrderDetails.Where(x => !orderDetailsDto.Contains(x)).ToList();
                _orderDetailRepository.RemoveRange(orderDetailsToRemove);
                _orderDetailRepository.UpdateRange(orderDetailsDto);
                await _unitOfWork.Commit();
                order.Total = (decimal)order.OrderDetails.Sum(x => x.Total);
                _orderRepository.Update(order);
                await _unitOfWork.Commit();
                return Ok(order);
            }
            return BadRequest(new BadRequestObjectResult("Null parameters received"));
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeDetailState(ChangeOrderDetailDto orderDetails)
        {
            if (orderDetails.DetailsId != null && orderDetails.DetailsId.Any(x => x != 0))
            {
                var details = await _orderDetailRepository.GetByRangeId(orderDetails.DetailsId);

                switch (orderDetails.Status)
                {
                    case 2:
                        //todo aqui debo de hacer el ajuste de inventario,disminuir el inventario

                        details.ForEach(x =>
                        {
                            if (x.Status.Equals("En Cola"))
                            {
                                x.Status = "En Proceso";
                            }
                        });
                        _orderDetailRepository.UpdateRange(details);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.DetailsInProcess(details);
                        return Ok(details);

                    case 3:
                        details.ForEach(x =>
                        {
                            if (x.Status.Equals("En Proceso"))
                            {
                                x.Status = "Finalizado";
                            }
                        });
                        _orderDetailRepository.UpdateRange(details);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.DetailsFinished(details);
                        return Ok(details);

                    case 4:
                        details.ForEach(x =>
                        {
                            if (x.Status.Equals("Finalizado"))
                            {
                                x.Status = "Entregado";
                            }
                        });
                        _orderDetailRepository.UpdateRange(details);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.DetailsFinished(details);
                        return Ok(details);
                    default:
                        return BadRequest(new BadRequestObjectResult("Invalid Status Provided"));
                }
            }

            return BadRequest(new BadRequestObjectResult("Null Value Detected in details"));
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeOrderState(ChangeOrderDetailDto orderToChange)
        {
            if (orderToChange.DetailsId != null && orderToChange.DetailsId.Any(x => x != 0))
            {
                var orders = await _orderRepository.GetByRangeId(orderToChange.DetailsId);
                bool allFinished = new bool();

                switch (orderToChange.Status)
                {
                    case 2:
                        orders.ForEach(x =>
                        {
                             allFinished = x.OrderDetails.Any(y => y.Status.Equals("Entregado"));
                            if (x.Status.Equals("Abierta" ) && allFinished)
                            {
                                x.Status = "Preventa";
                            }

                        });
                        if (allFinished !=true )
                        {
                            return BadRequest(new BadRequestObjectResult("Not all the details are in state Entregado"));
                        }

                        if (orders.Any(x=>x.Status.Equals("Abierta")))
                        {
                            return BadRequest(new BadRequestObjectResult("Any of the Orders are not in state Abierta"));

                        }
                        _orderRepository.UpdateRange(orders);
                        await _unitOfWork.Commit();
                        return Ok(orders);
                    case 3:
                        orders.ForEach(x =>
                        {
                             allFinished = x.OrderDetails.Any(y => y.Status.Equals("Entregado"));
                            if (x.Status.Equals("Preventa") && allFinished)
                            {
                                x.Status = "Cobrado";
                            }

                        });
                        if (allFinished != true)
                        {
                            return BadRequest(new BadRequestObjectResult("Not all the details are in state Entregado"));
                        }

                        if (orders.Any(x => x.Status.Equals("Preventa")))
                        {
                            return BadRequest(new BadRequestObjectResult("Any of the Orders are not in state Preventa"));

                        }
                        _orderRepository.UpdateRange(orders);
                        await _unitOfWork.Commit();
                        return Ok();

                    default:
                        return BadRequest(new BadRequestObjectResult("Invalid Status Provided"));
                }
            }
            return BadRequest(new BadRequestObjectResult("Null Value Detected in details"));
        }


    }
}