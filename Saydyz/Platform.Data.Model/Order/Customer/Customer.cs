using Platform.Data.Model.Order.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Model.Customer
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public string PhoneNo { get; set; }
        public int CustomerTypeId { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        
    }
}
