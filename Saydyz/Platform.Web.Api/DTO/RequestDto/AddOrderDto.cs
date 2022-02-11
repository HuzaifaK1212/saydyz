using System.Collections.Generic;

namespace Platform.Web.Api.DTO.RequestDto
{
    public class AddOrderDto
    {
        public AddOrderDto()
        {
            OrderItems = new HashSet<AddOrderItemDto>();
        }
        public string OrderCode { get; set; }
        public string CreatedOn { get; set; }
        public virtual AddCustomerDto Customer { get; set; }
        public string Channel { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string DeliveryCharge { get; set; }
        public string SalePrice { get; set; }

        public virtual ICollection<AddOrderItemDto> OrderItems { get; set; }
    }
}
