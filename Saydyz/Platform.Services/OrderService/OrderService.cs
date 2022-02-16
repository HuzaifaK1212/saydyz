using Platform.Data.Model.Customer;
using Platform.Data.Model.Flavors;
using Platform.Data.Model.Order;
using Platform.Data.Model.Order.Customer;
using Platform.Data.Repositories.Interfaces;
using Platform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Response<Customer>> AddCustomer(Customer customer)
        {
            try
            {
                customer.CreatedOn = DateTime.Now;
                customer.Active = true;
                var query = await orderRepository.AddCustomer(customer);
                return new Response<Customer>()
                {
                    Success = true,
                    Message = $"Customer added successfully. Id: {query.Id}",
                    Data = query
                };
            }
            catch (Exception ex)
            {
                return new Response<Customer>
                {
                    Success = false,
                    Message = $"Customer adding failed. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<Order>> AddOrder(Order order)
        {
            try
            {
                var query = await orderRepository.AddOrder(order);
                return new Response<Order>()
                {
                    Success = true,
                    Message = $"Order added successfully. Id: {query.Id}",
                    Data = query
                };
            }
            catch (Exception ex)
            {
                return new Response<Order>()
                {
                    Success = false,
                    Message = $"Order adding failed. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<OrderItem>> AddOrderItem(OrderItem orderItem)
        {
            try
            {
                orderItem.CreatedOn = DateTime.Now;
                orderItem.Active = true;
                var query = await orderRepository.AddOrderItem(orderItem);
                return new Response<OrderItem>()
                {
                    Success = true,
                    Message = $"Order Item added successfully",
                    Data = query
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderItem>()
                {
                    Success = true,
                    Message = $"Order Item adding failed. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<Customer>>> GetAllCustomers()
        {
            try
            {
                var query = await orderRepository.GetAllCustomers();
                if(query.Count > 0)
                {
                    return new Response<List<Customer>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched Customers. Count: {query.Count}",
                        Data = query
                    };
                } else
                {
                    return new Response<List<Customer>>()
                    {
                        Success = true,
                        Message = "No Customers found in Database"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Customer>>()
                {
                    Success = false,
                    Message = $"Failed fetching Customers. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<CustomerType>>> GetAllCustomerTypes()
        {
            try
            {
                var query = await orderRepository.GetAllCustomerTypes();
                if (query.Count > 0)
                {
                    return new Response<List<CustomerType>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched Customer types. Count: {query.Count}",
                        Data = query
                    };
                }
                else
                {
                    return new Response<List<CustomerType>>()
                    {
                        Success = true,
                        Message = "No Customer types found in Database"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<CustomerType>>()
                {
                    Success = false,
                    Message = $"Failed fetching Customer types. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<Flavor>>> GetAllFlavorsViaFlavorCode(string flavorCode)
        {
            try
            {
                var query = await orderRepository.GetAllFlavors();
                if (query.Count > 0)
                {

                    var selectiveFlavors = flavorCode.Length > 0 ? query
                                            .Where(x => x.Code == flavorCode)
                                            .ToList() : null;

                    return new Response<List<Flavor>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched " +
                                  selectiveFlavors
                                    .Select(x => x.Name)
                                    .FirstOrDefault() ?? flavorCode 
                                  + $" flavors. Count: {query.Count}",
                        Data = flavorCode.Length > 0 ? selectiveFlavors : query
                    };
                }
                else
                {
                    return new Response<List<Flavor>>()
                    {
                        Success = true,
                        Message = $"No {flavorCode} flavors found in Database"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Flavor>>()
                {
                    Success = false,
                    Message = $"Failed fetching {flavorCode} flavors. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<Flavor>>> GetAllFlavors()
        {
            try
            {
                var query = await orderRepository.GetAllFlavors();
                if (query.Count > 0)
                {

                    return new Response<List<Flavor>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched flavors. Count: {query.Count}",
                        Data = query
                    };
                }
                else
                {
                    return new Response<List<Flavor>>()
                    {
                        Success = true,
                        Message = "No flavors found in Database"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Flavor>>()
                {
                    Success = false,
                    Message = $"Failed fetching flavors. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<Area>>> GetAllAreas()
        {
            try
            {
                var query = await orderRepository.GetAllAreas();
                if (query.Count > 0)
                {

                    return new Response<List<Area>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched areas. Count: {query.Count}",
                        Data = query
                    };
                }
                else
                {
                    return new Response<List<Area>>()
                    {
                        Success = true,
                        Message = "No areas found in Database"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Area>>()
                {
                    Success = false,
                    Message = $"Failed fetching areas. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<Channel>>> GetAllChannels()
        {
            try
            {
                var query = await orderRepository.GetAllChannels();
                if (query.Count > 0)
                {

                    return new Response<List<Channel>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched channels. Count: {query.Count}",
                        Data = query
                    };
                }
                else
                {
                    return new Response<List<Channel>>()
                    {
                        Success = true,
                        Message = "No channels found in Database"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Channel>>()
                {
                    Success = false,
                    Message = $"Failed fetching channels. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<OrderItem>>> GetAllOrderItems()
        {
            try
            {
                var query = await orderRepository.GetAllOrderItems();

                if(query.Count > 0)
                {
                    return new Response<List<OrderItem>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched Order Items. Count: {query.Count}",
                        Data = query
                    };
                }
                else
                {
                    return new Response<List<OrderItem>>()
                    {
                        Success = true,
                        Message = $"No Order Items found.",
                        Data = query
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<OrderItem>>()
                {
                    Success = false,
                    Message = $"Failed to fetch Order items. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<OrderItem>>> GetAllOrderItemsViaOrderId(int orderId)
        {
            try
            {
                var query = await orderRepository.GetAllOrderItemsViaOrderId(orderId);
                if(query.Count > 0)
                {
                    return new Response<List<OrderItem>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched Order Items for Order Id: {orderId}. Count: {query.Count}",
                        Data = query
                    };
                }
                else
                {
                    return new Response<List<OrderItem>>()
                    {
                        Success = true,
                        Message = $"No order items for Order Id: {orderId}",
                        Data = query
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<OrderItem>>()
                {
                    Success = false,
                    Message = $"Failed to fetch Order items for Order Id: {orderId}. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<Order>>> GetAllOrders()
        {
            try
            {
                var query = await orderRepository.GetAllOrders();
                if(query.Count > 0)
                {
                    return new Response<List<Order>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched all Orders. Count: {query.Count}",
                        Data = query
                    };
                }
                else
                {
                    return new Response<List<Order>>()
                    {
                        Success = true,
                        Message = $"No orders found in database.",
                        Data = query
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Order>>()
                {
                    Success = false,
                    Message = $"Failed to fetch Orders. Reason: {ex.Message}"
                };
            }
        }



        public async Task<Response<List<Order>>> GetOrderViaCustomerPhoneNo(string phoneNo)
        {
            try
            {
                var customers = await orderRepository.GetCustomerViaPhoneNo(phoneNo);
                List<Order> filteredOrders = new List<Order>();
                if(customers.Count > 0)
                {
                    var orders = await orderRepository.GetAllOrders();
                    filteredOrders = orders.Where(x => x.Customer.PhoneNo == phoneNo).ToList();

                }

                if (filteredOrders.Count > 0)
                {
                    return new Response<List<Order>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched all customers with Contact: {phoneNo}. Count: {filteredOrders.Count}",
                        Data = filteredOrders
                    };
                }
                else
                {
                    {
                        return new Response<List<Order>>()
                        {
                            Success = true,
                            Message = $"No customers found Contact: {phoneNo}."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Order>>()
                {
                    Success = true,
                    Message = $"Failed to fetch customers with Contact: {phoneNo}.Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<Order>> GetOrderViaId(int id)
        {
            try
            {
                var query = await orderRepository.GetOrderViaId(id);
                return new Response<Order>()
                {
                    Success = query != null ? true : false,
                    Message = query != null ? $"Successfully fetched Order for Id: {id}." : " ",
                    Data = query
                };
            }
            catch (Exception ex)
            {
                return new Response<Order>()
                {
                    Success = false,
                    Message = $"Failed to fetch Order for Id: {id}. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<Order>> UpdateOrder(Order order)
        {
            try
            {
                var query = await orderRepository.UpdateOrder(order);
                return new Response<Order>()
                {
                    Success = query != null ? true : false,
                    Message = query != null ? "Successfully updated Order." : "Failed to update Order",
                    Data = query != null ? query : null
                };
            }
            catch (Exception ex)
            {
                return new Response<Order>()
                {
                    Success = false,
                    Message = $"Failed to update Order. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<OrderItem>> UpdateOrderItem(OrderItem orderItem)
        {
            try
            {
                var query = await orderRepository.UpdateOrderItem(orderItem);

                return new Response<OrderItem>()
                {
                    Success = query != null ? true : false,
                    Message = query != null ? "Successfully updated Order Item." : "Failed to update Order Item",
                    Data = query != null ? query : null
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderItem>()
                {
                    Success = false,
                    Message = $"Failed to update Order Item. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<Customer>> UpdateCustomer(Customer customer)
        {
            try
            {
                var query = await orderRepository.UpdateCustomer(customer);

                return new Response<Customer>()
                {
                    Success = query != null ? true : false,
                    Message = query != null ? "Successfully updated Customer" : "Failed to update Customer",
                    Data = query != null ? query : null
                };
            }
            catch (Exception ex)
            {
                return new Response<Customer>()
                {
                    Success = false,
                    Message = $"Failed to Update Customer. Reason: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<ItemType>>> GetAllItemTypes()
        {
            try
            {
                var query = await orderRepository.GetAllItemTypes();
                if (query.Count > 0)
                {
                    return new Response<List<ItemType>>()
                    {
                        Success = true,
                        Message = $"Successfully fetched all Item Types. Count: {query.Count}",
                        Data = query
                    };
                }
                else
                {
                    return new Response<List<ItemType>>()
                    {
                        Success = true,
                        Message = $"No Item types found in database.",
                        Data = query
                    };
                }

            }
            catch (Exception ex)
            {
                return new Response<List<ItemType>>()
                {
                    Success = false,
                    Message = $"Failed to fetch Item types. Reason: {ex.Message}"
                };
            }
        }
    }
}
