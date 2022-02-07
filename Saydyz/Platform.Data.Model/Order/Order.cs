using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.Model.Order
{
    public class Order : BaseEntity
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Channel { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string DeliveryCharge { get; set; }
        public string SalePrice { get; set; }
    }
}
