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
using Redpeper.Repositories.Tables;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHubContext<OrderHub, IOrderClient> _orderHub;

        private readonly IOrderDetailRepository _orderDetailRepository;
        private ITableRepository _tableRepository;
        private readonly IUnitOfWork _unitOfWork;


        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,
            IUnitOfWork unitOfWork,
            IHubContext<OrderHub, IOrderClient> orderHub, ITableRepository tableRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
            _orderHub = orderHub;
            _tableRepository = tableRepository;
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
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> UpdateOrderDetails(int id,[FromBody]List<OrderDetail> orderDetailsToUpdate)
        {
            if (id !=0 && orderDetailsToUpdate.Count>0)
            {

                var orderAsNoTracking = await _orderRepository.GetByIdNoTracking(id);
                if (orderAsNoTracking==null)
                {
                    return BadRequest(new {errors="This order doesn't exist", id });
                }

                if (orderDetailsToUpdate.Any(x=> x.OrderId!= id))
                {
                    return BadRequest(new { errors = "The order id Provided  not match with  order Details", id, orderDetailsToUpdate });

                }

                var orderDetailsToRemove = orderAsNoTracking.OrderDetails.Where(x => !orderDetailsToUpdate.Select(y=>y.Id).Contains(x.Id) && x.Status.Equals("En Cola")).ToList();
                if (orderDetailsToRemove.Count>=1)
                {
                    _orderDetailRepository.RemoveRange(orderDetailsToRemove);
                }

                var orderDetailsNotValidState = orderAsNoTracking.OrderDetails.Where(x => orderDetailsToUpdate.Select(y => y.Id).Contains(x.Id) && !x.Status.Equals("En Cola")).ToList();
                if (orderDetailsNotValidState.Count>=1)
                {
                    return BadRequest(new {errors = "Only send details with state 'En Cola', the update was not applied ", orderDetailsNotValidState});
                }
                orderDetailsToUpdate.ForEach(x=>x.Status= "En Cola");
                _orderDetailRepository.UpdateRange(orderDetailsToUpdate);
                await _unitOfWork.Commit();

                var order = await _orderRepository.GetById(id);
                order.Total = (decimal)orderDetailsToUpdate.Sum(x => x.Total);
                _orderRepository.Update(order);
                await _unitOfWork.Commit();
                await _orderHub.Clients.All.DetailsUpdated(order);
                return Ok(new{orderDetailsToUpdate, orderDetailsToRemove});
            }
            return BadRequest(new BadRequestObjectResult(new { errors = "Null parameter received", orderDetailsToUpdate }));
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeDetailState(ChangeOrderDetailDto orderDetails)
        {
            if (orderDetails.DetailsId != null && orderDetails.DetailsId.Any(x => x != 0))
            {
                var details = await _orderDetailRepository.GetByRangeId(orderDetails.DetailsId);
                var detailsWithDifferentState = new List<OrderDetail>();
                switch (orderDetails.Status)
                {
                    case 2:
                        //todo aqui debo de hacer el ajuste de inventario,disminuir el inventario

                        details.ForEach(x => { if (x.Status.Equals("En Cola")) { x.Status = "En Proceso"; }else { detailsWithDifferentState.Add(x); } });
                       
                        if (detailsWithDifferentState.Count >=1)
                        {
                            return BadRequest(new { errors = "Only send details with state 'En Cola'", detailsWithDifferentState });

                        }

                        _orderDetailRepository.UpdateRange(details);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.DetailsInProcess(details);
                        return Ok(details);

                    case 3:
                        details.ForEach(x => { if (x.Status.Equals("En Proceso")) { x.Status = "Finalizado"; }else { detailsWithDifferentState.Add(x); } });

                        if (detailsWithDifferentState.Count >= 1)
                        {
                            return BadRequest(new { errors = "Only send details with state 'En Proceso'", detailsWithDifferentState });

                        }
                        _orderDetailRepository.UpdateRange(details);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.DetailsFinished(details);
                        return Ok(details);

                    case 4:
                        details.ForEach(x => { if (x.Status.Equals("Finalizado")) { x.Status = "Entregado"; }else { detailsWithDifferentState.Add(x); } });
                        
                        if (detailsWithDifferentState.Count >= 1)
                        {
                            return BadRequest(new { errors = "Only send details with state 'Finalizado'", detailsWithDifferentState });

                        }
                        _orderDetailRepository.UpdateRange(details);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.DetailsDelivered(details);
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

                var incompletedOrders = new List<Order>();
                var incompletedDetails = new List<OrderDetail>();

                switch (orderToChange.Status)
                {
                    case 2:
                        orders.ForEach(x =>
                        {
                            incompletedDetails = x.OrderDetails.Where(y => !y.Status.Equals("Entregado")).ToList();
                            if (x.Status.Equals("Abierta" ) && incompletedDetails.Count==0)
                            {
                                x.Status = "Preventa";
                            }
                            else
                            {
                                incompletedOrders.Add(x);
                            }

                        });
                        if (incompletedDetails.Count >=1 )
                        {
                            return BadRequest(new BadRequestObjectResult(new { errors = "Not all the details are in state 'Entregado'", incompletedDetails }));
                        }

                        if (orders.Any(x=>x.Status.Equals("Abierta")))
                        {
                            return BadRequest(new BadRequestObjectResult(new{errors="Any of the Orders are not in state Abierta", incompletedOrders }));

                        }
                        _orderRepository.UpdateRange(orders);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.PreSaleOrders(orders);
                        return Ok(orders);
                    case 3:
                        orders.ForEach(x =>
                        {
                            incompletedDetails = x.OrderDetails.Where(y => !y.Status.Equals("Entregado")).ToList();

                            if (x.Status.Equals("Preventa")  && incompletedDetails.Count == 0)
                            {
                                x.Status = "Cobrado";
                            }

                        });
                        if (incompletedDetails.Count >=1 )
                        {
                            return BadRequest(new BadRequestObjectResult(new { errors = "Not all the details are in state 'Entregado'", incompletedDetails }));
                        }

                        if (orders.Any(x => x.Status.Equals("Preventa")))
                        {
                            return BadRequest(new BadRequestObjectResult(new {errors="Any of the Orders are not in state Preventa",orders}));

                        }
                        _orderRepository.UpdateRange(orders);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.ChargedOrders(orders);
                        var tablesId = orders.Select(x => x.TableId).ToList();
                        var tables = await _tableRepository.GetByIdRange(tablesId);
                        await _orderHub.Clients.All.FreeTable(tables);
                        return Ok();

                    default:
                        return BadRequest(new BadRequestObjectResult("Invalid Status Provided"));
                }
            }
            return BadRequest(new BadRequestObjectResult("Null Value Detected in details"));
        }

        
    }
}