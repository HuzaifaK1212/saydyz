using Microsoft.EntityFrameworkCore;
using Platform.Data.Model.Customer;
using Platform.Data.Model.Flavors;
using Platform.Data.Model.Order;
using Platform.Data.Repositories.Context;
using Platform.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Repositories
{
    public class OrderRepository : BaseRepository<Order, OrderContext>, IOrderRepository
    {
        private DbSet<Order> Order;
        private DbSet<OrderItem> OrderItem;
        private DbSet<Customer> Customer;
        private DbSet<CustomerType> CustomerType;
        private DbSet<Flavor> Flavor;

        public OrderRepository(IUnitOfWork<OrderContext> uow) : base(uow)
        {
            var context = (OrderContext)uow.Context;
            Order = context.Order;
            OrderItem = context.OrderItem;
            Customer = context.Customer;
            CustomerType = context.CustomerType;
            Flavor = context.Flavor;

            Order.AsNoTracking();
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            Customer.Add(customer);
            var saved = _uow.Commit();
            return saved > 0 ? customer : null;
        }

        public async Task<Order> AddOrder(Order order)
        {
            Order.Add(order);
            var saved = _uow.Commit();
            return saved > 0 ? order : null;
        }

        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
            OrderItem.Add(orderItem);
            var saved = _uow.Commit();
            return saved > 0 ? orderItem : null;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await Customer
                            .Include(x => x.CustomerType)
                            .ToListAsync();
        }

        public async Task<List<CustomerType>> GetAllCustomerTypes()
        {
            return await CustomerType
                            .ToListAsync();
        }

        public async Task<List<Flavor>> GetAllFlavors()
        {
            return await Flavor
                            .Include(x => x.ItemType)
                            .ToListAsync();
        }

        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            return await OrderItem
                            .Include(x => x.Flavor)
                                .ThenInclude(xx => xx.ItemType)
                            .ToListAsync();
        }

        public async Task<List<OrderItem>> GetAllOrderItemsViaOrderId(int orderId)
        {
            return await OrderItem
                            .Include(x => x.Flavor)
                                .ThenInclude(xx => xx.ItemType)
                            .Where(x => x.OrderId == orderId)
                            .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await Order
                            .Include(x => x.Customer)
                                .ThenInclude(xx => xx.CustomerType)
                            .Include(x => x.OrderItems)
                                .ThenInclude(xx => xx.Flavor)
                                    .ThenInclude(xxx => xxx.ItemType)
                            .ToListAsync();
        }


        public async Task<List<Customer>> GetCustomerViaPhoneNo(string phoneNo)
        {
            return await Customer
                            .Include(x => x.CustomerType)
                            .Where(x => x.PhoneNo == phoneNo)
                            .ToListAsync();
        }

        public async Task<Order> GetOrderViaId(int id)
        {
            return await Order
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var orderItems = await OrderItem
                                    .Where(x => x.OrderId == order.Id)
                                    .ToListAsync();
            orderItems.ForEach(x => OrderItem
                                        .Remove(x));

            Order.Update(order);
            var saved = _uow.Commit();
            return saved > 0 ? order : null;
        }

        public async Task<OrderItem> UpdateOrderItem(OrderItem orderItem)
        {
            OrderItem.Update(orderItem);
            var saved = _uow.Commit();
            return saved > 0 ? orderItem : null;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            Customer.Update(customer);
            var saved = _uow.Commit();
            return saved > 0 ? customer : null;
        }
    }
}
