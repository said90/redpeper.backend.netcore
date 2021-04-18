using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Redpeper.Dto;
using Redpeper.Extensions;
using Redpeper.Hubs;
using Redpeper.Hubs.Clients;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Services.Expo;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHubContext<OrderHub, IOrderClient> _orderHub;
        private readonly IUnitOfWork _unitOfWork;
        private IExpoServices _expoServices;


        public OrderController(IUnitOfWork unitOfWork, IHubContext<OrderHub, IOrderClient> orderHub, IExpoServices expoServices)
        {
            _unitOfWork = unitOfWork;
            _orderHub = orderHub;
            _expoServices = expoServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            return await _unitOfWork.OrderRepository.GetActiveOrders();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            return await _unitOfWork.OrderRepository.GetByIdWithDetails(id);
        }

        [HttpGet("[action]/{orderNumber}")]
        public async Task<ActionResult<Order>> GetByOrderNumber(string orderNumber)
        {
            return await _unitOfWork.OrderRepository.GetByOrderNumber(orderNumber);
        }

        [HttpGet("[action]/{status}")]
        public async Task<ActionResult<List<Order>>> GetOrderByStatus(string status)
        {
            return await _unitOfWork.OrderRepository.GetOrdersByStatus(status);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var or = new Order
                    {
                        CustomerId = order.CustomerId,
                        Date = DateTime.Now,
                        Total = order.Total,
                        OrderTypeId = order.OrderType,
                        Status = "Abierta",
                        NotificationToken = order.NotificationToken
                    };
                    var table = await _unitOfWork.TableRepository.GetByIdTask(order.TableId);

                    if (table != null)
                    {

                        if (table.State == 1)
                        {
                            return Conflict(new { table = table.Id, message = "Order Already Created" });
                        }
                        or.TableId = table.Id;
                        
                    }


                    order.Date = DateTime.Now;
                    order.Status = "Abierta";
                    or.OrderNumber = "O-" + (await _unitOfWork.OrderRepository.CountTask() + 1);
                    await _unitOfWork.OrderRepository.InsertTask(or);
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
                        UnitPrice = x.UnitPrice,
                    }).ToList();


                    await _unitOfWork.OrderDetailRepository.InsertRangeTask(details);

                    await _unitOfWork.Commit();

                    order.OrderDetails = details;
                    order.Id = or.Id;
                    order.OrderNumber = or.OrderNumber;
                    await _orderHub.Clients.All.OrderCreated(order);
          
                    if (table != null)
                    {
                        table.State = 1;
                        _unitOfWork.TableRepository.Update(table);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.BussyTable(table);


                    }
                    return Ok(order);
                }
                else
                {
                    return BadRequest(order);
                }
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
                var orderAsNoTracking = await _unitOfWork.OrderRepository.GetByIdNoTracking(id);
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
                    _unitOfWork.OrderDetailRepository.RemoveRange(orderDetailsToRemove);
                }

                var orderDetailsNotValidState = orderAsNoTracking.OrderDetails.Where(x => orderDetailsToUpdate.Select(y => y.Id).Contains(x.Id) && !x.Status.Equals("En Cola")).ToList();
                if (orderDetailsNotValidState.Count>=1)
                {
                    return BadRequest(new {errors = "Only send details with state 'En Cola', the update was not applied ", orderDetailsNotValidState});
                }
                orderDetailsToUpdate.ForEach(x=>
                {
                    x.Status = "En Cola";
                });
                _unitOfWork.OrderDetailRepository.UpdateRange(orderDetailsToUpdate);
                await _unitOfWork.Commit();

                var order = await _unitOfWork.OrderRepository.GetByIdWithDetails(id);
                order.Total = (decimal)order.OrderDetails.Sum(x => x.Total);
                _unitOfWork.OrderRepository.Update(order);
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
                var details = await _unitOfWork.OrderDetailRepository.GetByRangeId(orderDetails.DetailsId);
                var orderNumber = await _unitOfWork.OrderRepository.GetOrderNumber(details[0].OrderId);
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

                        _unitOfWork.OrderDetailRepository.UpdateRange(details);
                        var inventoryTransactions = new List<InventorySupplyTransaction>();
                        details.ForEach(x =>
                        {
                            if (x.ComboId!=null)
                            {
                                x.Combo.ComboDetails.ForEach(y =>
                                {
                                   y.Dish.DishSupplies.ForEach(z =>
                                   {
                                       var inventorySupplyTransaction = new InventorySupplyTransaction
                                       {
                                           TransactionType = 1,
                                           TransactionNumber = orderNumber,
                                           Date = DateTime.Now,
                                           ExpirationDate = DateTime.Now,
                                           Qty = -z.Qty*y.Qty,
                                           SupplyId = z.SupplyId
                                       };
                                       inventoryTransactions.Add(inventorySupplyTransaction);
                                   });
                                });
                            }
                            else
                            {
                                x.Dish.DishSupplies.ForEach(y =>
                                {
                                    var inventorySupplyTransaction = new InventorySupplyTransaction
                                    {
                                        TransactionType = 1,
                                        TransactionNumber = orderNumber,
                                        Date = DateTime.Now,
                                        ExpirationDate = DateTime.Now,
                                        Qty =-y.Qty*x.Qty,
                                        SupplyId = y.SupplyId
                                    };
                                    inventoryTransactions.Add(inventorySupplyTransaction);
                                });
                            }
                        });
                        await _orderHub.Clients.All.DetailsInProcess(details);
                        await _unitOfWork.InventorySupplyTransactionRepository.InsertRangeTask(inventoryTransactions);
                        await _unitOfWork.Commit();
                        return Ok(details);

                    case 3:
                        details.ForEach(x => { if (x.Status.Equals("En Proceso")) { x.Status = "Finalizado"; }else { detailsWithDifferentState.Add(x); } });

                        if (detailsWithDifferentState.Count >= 1)
                        {
                            return BadRequest(new { errors = "Only send details with state 'En Proceso'", detailsWithDifferentState });

                        }
                        _unitOfWork.OrderDetailRepository.UpdateRange(details);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.DetailsFinished(details);

                        var orderIds = details.Select(x => x.OrderId).ToList();

                        var orders = await _unitOfWork.OrderRepository.GetByRangeIdNoIncludes(orderIds);

                        var notifications = details.Select(x => new NotificationOrderDto
                        {
                            OrderDetail = x,
                            Title = "Platillo Listo Para Entregar",
                            Token = orders.Where(y=> y.Id == x.OrderId).Select(y=> y.NotificationToken).FirstOrDefault()
                        }).ToList();

                        await _expoServices.SendPushNotification(notifications);
                        return Ok(details);

                    case 4:
                        details.ForEach(x => { if (x.Status.Equals("Finalizado")) { x.Status = "Entregado"; }else { detailsWithDifferentState.Add(x); } });
                        
                        if (detailsWithDifferentState.Count >= 1)
                        {
                            return BadRequest(new { errors = "Only send details with state 'Finalizado'", detailsWithDifferentState });

                        }
                        _unitOfWork.OrderDetailRepository.UpdateRange(details);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.DetailsDelivered(details);
                        return Ok(details);
                    default:
                        return BadRequest(new BadRequestObjectResult("Invalid Status Provided"));
                }
            }

            return BadRequest(new BadRequestObjectResult("Null Value Detected in details"));
        }

        [HttpPatch("[action]")] //Espero los Id de las ordenes que cambiaran de estado a preventa o cobrado
        public async Task<IActionResult> ChangeOrderState(ChangeOrderDetailDto orderToChange)
        {
            if (orderToChange.DetailsId != null && orderToChange.DetailsId.Any(x => x != 0))
            {
                var orders = await _unitOfWork.OrderRepository.GetByRangeId(orderToChange.DetailsId);

                var incompletedOrders = new List<Order>();
                var incompletedDetails = new List<OrderDetail>();

                switch (orderToChange.Status)
                {
                    //Preventa
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
                        _unitOfWork.OrderRepository.UpdateRange(orders);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.PreSaleOrders(orders);
                        return Ok(orders);

                    //Cobrado
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
                        _unitOfWork.OrderRepository.UpdateRange(orders);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.ChargedOrders(orders);
                        var tablesId = orders.Select(x => x.TableId).ToList();
                        var tables = await _unitOfWork.TableRepository.GetByIdRange(tablesId);
                        tables.ForEach(x =>
                        {

                            x.CustomerId = null;
                            x.State = 0;
                            x.Description = x.Description;
                            x.CustomerLastName = null;
                            x.CustomerName = null;
                        });
                        _unitOfWork.TableRepository.UpdateRange(tables);
                        await _unitOfWork.Commit();
                        await _orderHub.Clients.All.FreeTable(tables);
                        return Ok();
                    case 4:
                        orders.ForEach(x =>
                        {
                            x.Status = "Anulado";
                            x.OrderDetails.ForEach(y => { y.Status = "Anulado"; });
                        });
                        return Ok(orders);

                    default:
                        return BadRequest(new BadRequestObjectResult("Invalid Status Provided"));
                }
            }
            return BadRequest(new BadRequestObjectResult("Null Value Detected in details"));
        }

        [HttpPatch("{orderId}/[action]/{tableId}")]
        public async Task<IActionResult> ChangeOrderTable(int orderId, int tableId)
        {
            var newTable= await _unitOfWork.TableRepository.GetByIdTask(tableId);
            if (newTable == null)
            {
                return NotFound(tableId);
            }

            if (newTable.State==1) 
            {
                return BadRequest(new BadRequestObjectResult(new { errors = "This table is bussy ", newTable }));
            }

            var order = await _unitOfWork.OrderRepository.GetByIdWithDetails(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }
            var tables = new List<Table>();

            var oldTable = await _unitOfWork.TableRepository.GetByIdTask(order.TableId);
            oldTable.CustomerId = null;
            oldTable.State = 0;
            _unitOfWork.TableRepository.Update(oldTable);

            order.TableId = tableId;
            newTable.State = 1;
            newTable.CustomerId = order.CustomerId;
            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.TableRepository.Update(newTable);
            await _unitOfWork.Commit();
            tables.Add(oldTable);
            tables.Add(newTable);
            await _orderHub.Clients.All.FreeTable(tables);
            return Ok(order);

        }

        [HttpDelete("{orderId}/orderDetail/{detailId}")]
        public async Task<IActionResult> DeleteDetail(int orderId, int detailId)
        {
            var orderAsNoTracking = await _unitOfWork.OrderRepository.GetByIdNoTracking(orderId);

            if (orderAsNoTracking ==null)
            {
                return NotFound(new {Id = orderId, message = "Order not found"});
            }

            var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdTask(detailId);

            if (orderDetail == null)
            {
                return NotFound(new { Id = orderDetail, message = "Order detail not found" });

            }

            await _unitOfWork.OrderDetailRepository.DeleteTask(detailId);
            await _unitOfWork.Commit();
            var order = await _unitOfWork.OrderRepository.GetByIdWithDetails(orderId);
            order.Total = (decimal)order.OrderDetails.Sum(x => x.Total);
            await _unitOfWork.Commit();
            return Ok(orderDetail);
        }

        //[HttpPost("{orderId}/[action]")]
        //public async Task<IActionResult> DivideOrder(int orderId,[FromBody] List<OrderDto> orders)
        //{

        //    if (!await _unitOfWork.OrderRepository.ExistAsync(orderId)) return BadRequest(orderId);


        //    Customer newCustomer = new Customer();
        //    List<Order> newOrders = new List<Order>();

        //    orders.ForEach(async x =>
        //    {
        //        if (x.CustomerId == 0)
        //        {
        //             newCustomer = new Customer {Name = x.Name, Lastname = x.LastName};
        //            await _unitOfWork.CustomerRepository.InsertTask(newCustomer);
        //        }

        //        var newOrder = new Order
        //        {
        //            CustomerId = x.CustomerId,
        //            TableId = x.TableId,
        //            Date = DateTime.Now,
        //            Total = x.Total,
        //            Status = "Preventa",
        //            Customer = x.CustomerId ==0? newCustomer:null
        //        };
        //        newOrder.OrderNumber = "O-" + (await _unitOfWork.OrderRepository.CountTask() + 1);

        //        await _unitOfWork.OrderRepository.InsertTask(newOrder);

        //        x.OrderDetails.ForEach(y => { y.Order = newOrder; });
        //        _unitOfWork.OrderDetailRepository.UpdateRange(x.OrderDetails);
        //        newOrders.Add(newOrder);
        //    });


        //    await _unitOfWork.Commit();
        //    var originalOrder = await _unitOfWork.OrderRepository.GetByIdNoTracking(orderId);

        //    originalOrder.Total = (decimal)originalOrder.OrderDetails.Sum(x => x.Total);
        //    _unitOfWork.OrderRepository.Update(originalOrder);
        //    await _unitOfWork.Commit();

        //    return Ok(new {originalOrder, newOrders });
        //}
     
    }
}