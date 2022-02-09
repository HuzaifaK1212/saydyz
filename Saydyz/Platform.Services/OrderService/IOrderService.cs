﻿using Platform.Data.Model.Customer;
using Platform.Data.Model.Flavors;
using Platform.Data.Model.Order;
using Platform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Services.OrderService
{
    public interface IOrderService
    {
        // Model: Order
        Task<Response<List<Order>>> GetAllOrders();
        Task<Response<Order>> GetOrderViaId(int id);
        Task<Response<Order>> AddOrder(Order order);
        Task<Response<Order>> UpdateOrder(Order order);

        // Model: OrderItem
        Task<Response<OrderItem>> AddOrderItem(OrderItem orderItem);
        Task<Response<OrderItem>> UpdateOrderItem(OrderItem orderItem);
        Task<Response<List<OrderItem>>> GetAllOrderItems();
        Task<Response<List<OrderItem>>> GetAllOrderItemsViaOrderId(int orderId);

        // Model: Flavor
        Task<Response<List<Flavor>>> GetAllFlavorsViaFlavorCode(string flavorCode);
        Task<Response<List<Flavor>>> GetAllFlavors();

        // Model: Customer
        Task<Response<Customer>> AddCustomer(Customer customer);
        Task<Response<List<Customer>>> GetAllCustomers();
        Task<Response<List<CustomerType>>> GetAllCustomerTypes();
        Task<Response<List<Customer>>> GetCustomerViaPhoneNo(string phoneNo);
        Task<Response<Customer>> UpdateCustomer(Customer customer);
    }
}
