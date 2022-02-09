using Platform.Data.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.Model.Flavors
{
    public class Flavor
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Price { get; set; }
        public int ItemTypeId { get; set; }
        public virtual ItemType ItemType { get; set; }
    }
}
