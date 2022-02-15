using Platform.Data.Model.Customer;
using Platform.Data.Model.Flavors;
using Platform.Data.Model.Order;
using Platform.Data.Model.Order.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        // Model: Order
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderViaId(int id);
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order order);

        // Model: OrderItem
        Task<OrderItem> AddOrderItem(OrderItem orderItem);
        Task<OrderItem> UpdateOrderItem(OrderItem orderItem);
        Task<List<OrderItem>> GetAllOrderItems();
        Task<List<OrderItem>> GetAllOrderItemsViaOrderId(int orderId);

        // Model: Flavor
        Task<List<Flavor>> GetAllFlavors();

        // Model: Customer
        Task<Customer> AddCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers();
        Task<List<CustomerType>> GetAllCustomerTypes();
        Task<List<Customer>> GetCustomerViaPhoneNo(string phoneNo);
        Task<Customer> UpdateCustomer(Customer customer);

        // Model: Area
        Task<List<Area>> GetAllAreas();

        // Model: Channel
        Task<List<Channel>> GetAllChannels();
    }
}
