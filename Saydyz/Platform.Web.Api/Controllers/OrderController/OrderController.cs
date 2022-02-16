using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Platform.Data.Model.Customer;
using Platform.Data.Model.Order;
using Platform.Services.OrderService;
using Platform.Utilities;
using Platform.Web.Api.DTO;
using Platform.Web.Api.DTO.RequestDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Web.Api.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private IConfiguration configuration;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this.orderService = orderService;
            this.mapper = mapper;
        }

        private IActionResult ErrorHandling(Exception ex)
        {
            if (ex.InnerException != null && ex.InnerException is DbUpdateException)
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = ex.InnerException.ToString(),
                    Error = new Error()
                    {
                        Code = ErrorCodes.DB_ERROR,
                        Cause = ex.Message + (ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message) ? $"INNEX [{ex.InnerException.Message}]" : "")
                    }
                });
            return BadRequest(new Response<int>()
            {
                Success = false,
                Message = $"Order adding failed due to [{ex.Message}]",
                Error = new Error()
                {
                    Code = ErrorCodes.UNKNOWN_ERROR,
                    Cause = ex.Message
                }
            });
        }

        [HttpGet]
        [Route("api/order/customer/all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var query = await orderService.GetAllCustomers();
                if (query.Success)
                {
                    return Ok(query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/customertype/all")]
        public async Task<IActionResult> GetAllCustomerTypes()
        {
            try
            {
                var query = await orderService.GetAllCustomerTypes();
                List<CustomerTypeDto> customerTypes = mapper.Map<List<CustomerTypeDto>>(query.Data);
                if (query.Success)
                {
                    return Ok(new Response<List<CustomerTypeDto>>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = customerTypes
                    });
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/flavor/all")]
        public async Task<IActionResult> GetAllFlavors()
        {
            try
            {
                var query = await orderService.GetAllFlavors();
                List<FlavorDto> flavors = mapper.Map<List<FlavorDto>>(query.Data);
                if (query.Success)
                {
                    return Ok(new Response<List<FlavorDto>>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = flavors
                    });
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/flavor/all/{flavorCode}")]
        public async Task<IActionResult> GetAllFlavorsViaFlavorCode([FromRoute] string flavorCode)
        {
            try
            {
                var query = await orderService.GetAllFlavorsViaFlavorCode(flavorCode);
                if (query.Success)
                {
                    return Ok(query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/orderitem/all")]
        public async Task<IActionResult> GetAllOrderItems()
        {
            try
            {
                var query = await orderService.GetAllOrderItems();
                if (query.Success)
                {
                    return Ok(query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/orderitem/all/{orderId}")]
        public async Task<IActionResult> GetAllOrderItemsViaOrderId([FromRoute] int orderId)
        {
            try
            {
                var query = await orderService.GetAllOrderItemsViaOrderId(orderId);
                if (query.Success)
                {
                    return Ok(query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/all")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var query = await orderService.GetAllOrders();
                List<OrderDto> orderList = mapper.Map<List<OrderDto>>(query.Data);
                
                if (query.Success)
                {
                    return Ok(new Response<List<OrderDto>>()
                    {
                        Success = query.Success,
                        Message = $"{orderList.Count} fetched successfully",
                        Data = orderList
                    });
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/customer/{phoneNo}")]
        public async Task<IActionResult> GetOrdersViaCustomerPhoneNo([FromRoute] string phoneNo)
        {
            try
            {
                var query = await orderService.GetOrderViaCustomerPhoneNo(phoneNo);
                List<OrderDto> customerOrders = mapper.Map<List<OrderDto>>(query.Data);
                if (query.Success)
                {
                    return Ok(new Response<List<OrderDto>>()
                    {
                        Success = query.Success,
                        Message = $"{customerOrders.Count} fetched successfully",
                        Data = customerOrders
                    });
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/{id}")]
        public async Task<IActionResult> GetOrderViaId([FromRoute] int id)
        {
            try
            {
                var query = await orderService.GetOrderViaId(id);
                if (query.Success)
                {
                    return Ok(query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/area/all")]
        public async Task<IActionResult> GetAllAreas()
        {
            try
            {
                var query = await orderService.GetAllAreas();
                if (query.Success)
                {
                    return Ok(query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/channel/all")]
        public async Task<IActionResult> GetAllChannels()
        {
            try
            {
                var query = await orderService.GetAllChannels();
                if (query.Success)
                {
                    return Ok(query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpGet]
        [Route("api/order/itemtype/all")]
        public async Task<IActionResult> GetAllItemTypes()
        {
            try
            {
                var query = await orderService.GetAllItemTypes();
                ItemTypeDto itemTypes = mapper.Map<ItemTypeDto>(query.Data);
                if (query.Success)
                {
                    return Ok(new Response<ItemTypeDto>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = itemTypes
                    });
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }


        [HttpPost]
        [Route("api/order/add")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderDto addOrder)
        {
            try
            {
                Order _addOrder = mapper.Map<Order>(addOrder);
                var query = await orderService.AddOrder(_addOrder);
                OrderDto _query = mapper.Map<OrderDto>(query.Data);

                if (query.Success)
                {
                    return Ok(_query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpPost]
        [Route("api/order/customer/add")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerDto addCustomer)
        {
            try
            {
                Customer _addCustomer = mapper.Map<Customer>(addCustomer);
                var query = await orderService.AddCustomer(_addCustomer);
                CustomerDto _query = mapper.Map<CustomerDto>(query.Data);
                if (query.Success)
                {
                    return Ok(_query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpPut]
        [Route("api/order/update")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDto updateOrder)
        {
            try
            {
                Order _updateOrder = mapper.Map<Order>(updateOrder);
                var query = await orderService.UpdateOrder(_updateOrder);
                OrderDto _query = mapper.Map<OrderDto>(query.Data);
                if (query.Success)
                {
                    return Ok(_query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpPut]
        [Route("api/order/orderitem/update")]
        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderDto updateOrder)
        {
            try
            {
                OrderItem _updateOrder = mapper.Map<OrderItem>(updateOrder);
                var query = await orderService.UpdateOrderItem(_updateOrder);
                OrderItemDto _query = mapper.Map<OrderItemDto>(query.Data);
                if (query.Success)
                {
                    return Ok(_query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }

        [HttpPut]
        [Route("api/order/customer/update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto updateCustomer)
        {
            try
            {
                Customer _updateCustomer = mapper.Map<Customer>(updateCustomer);
                var query = await orderService.UpdateCustomer(_updateCustomer);
                CustomerDto _query = mapper.Map<CustomerDto>(query.Data);
                if (query.Success)
                {
                    return Ok(_query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return ErrorHandling(ex);
            }
        }
    }
}
