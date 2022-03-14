using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]// makes validation
    [Route("api/[controller]")]

    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<KitOrder>> CreateOrder(OrderDto orderDto)
        {
            var order = new KitOrder();
            try
            {
                var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                // var address = new Address()
                // {
                //     FirstName = orderDto.ShipToAddress.FirstName,
                //     LastName = orderDto.ShipToAddress.LastName,
                //     Street = orderDto.ShipToAddress.Street,
                //     City = orderDto.ShipToAddress.City,
                //     ZipCode = orderDto.ShipToAddress.ZipCode
                // };

                order = await _orderService.CreateOrder(email, orderDto.Items);

                if (order == null) throw new Exception();

            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong"));
            }
            return Ok(order);
        }


        [HttpPut("update")]
        public async Task<ActionResult<KitOrder>> UpdateOrder(UpdateOrderDto orderDto)
        {
            var order = new KitOrder();
            try
            {
                var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                order = await _orderService.UpdateOrder(orderDto.OrderId, orderDto.HistoryId, orderDto.Items, email);
                if (order == null) throw new Exception();
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong"));
            }

            return Ok(order);
        }



        [HttpPut("accept")]
        public async Task<ActionResult<KitOrder>> AcceptOrder(AcceptOrderDto orderDto)
        {
            var order = new KitOrder();
            try
            {
                var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                order = await _orderService.AcceptOrder(orderDto.OrderId, orderDto.HistoryId, email);
                if (order == null) throw new Exception();
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong"));
            }

            return Ok(order);
        }

        [HttpPut("comment")]
        public async Task<ActionResult<KitOrder>> CommentOrder(CommentOrderDto orderDto)
        {
            var order = new KitOrder();
            try
            {
                var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                order = await _orderService.CommentOrder(orderDto.OrderId, orderDto.HistoryId, email, orderDto.Comment, orderDto.AcceptedItemsId, orderDto.PendingItemsId);
                if (order == null) throw new Exception();
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong"));
            }

            return Ok(order);
        }




        [HttpGet]
        public async Task<ActionResult<List<KitOrder>>> GetOrdersByUser()
        {
            var orders = new List<KitOrder>();
            try
            {
                var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                orders = await _orderService.GetOrdersByUser(email);
                if (orders == null) throw new Exception();
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong"));
            }

            return Ok(orders);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<KitOrder>> GetOrderById([FromRoute] int id)
        {
            var order = new KitOrder();
            try
            {
                var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
                order = await _orderService.GetOrderById(id);
                if (order == null)
                {
                    throw new Exception();
                }
                if (order.FarmerEmail != email && role != "cooperative")
                {
                    return BadRequest(new ApiResponse(401));
                }

            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong"));
            }

            return Ok(order);
        }


        [HttpGet("history/{Id:int}")]
        public async Task<ActionResult<KitOrderHistory>> GetHistoryById([FromRoute] int id)
        {
            var history = new KitOrderHistory();
            try
            {
                var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
                history = await _orderService.GetHistoryById(id);
                if (history == null)
                {
                    throw new Exception();
                }
                if (history.FarmerEmail != email && role != "cooperative")
                {
                    return BadRequest(new ApiResponse(401));
                }
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong"));
            }

            return Ok(history);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<KitOrder>>> GetAllOrders()
        {
            var orders = new List<KitOrder>();
            try
            {
                var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

                if (role != "cooperative")
                {
                   return BadRequest(new ApiResponse(401));
                }
                orders = await _orderService.GetAllOrders();
                if (orders == null) throw new Exception();
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong"));
            }

            return Ok(orders);
        }

    }
}