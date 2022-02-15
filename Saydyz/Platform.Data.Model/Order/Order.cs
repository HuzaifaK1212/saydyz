using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.Model.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        public string OrderCode { get; set; }
        public int CustomerId { get; set; }
        public virtual Model.Customer.Customer Customer { get; set; }
        public int ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string DeliveryCharge { get; set; }
        public string SalePrice { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
