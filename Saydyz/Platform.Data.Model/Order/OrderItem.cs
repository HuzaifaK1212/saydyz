using Platform.Data.Model.Flavors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.Model.Order
{
    public class OrderItem : BaseEntity
    {
        public string Quantity { get; set; }
        public string Price { get; set; }
        public int FlavorId { get; set; }
        public virtual Flavor Flavor { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public bool IsPromo { get; set; }

    }
}
