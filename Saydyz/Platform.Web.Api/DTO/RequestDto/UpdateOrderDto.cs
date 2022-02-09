using System.Collections.Generic;

namespace Platform.Web.Api.DTO.RequestDto
{
    public class UpdateOrderDto : BaseDto
    {
        public UpdateOrderDto()
        {
            OrderItems = new HashSet<UpdateOrderItemDto>();
        }
        public virtual AddCustomerDto Customer { get; set; }
        public string Channel { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string DeliveryCharge { get; set; }
        public string SalePrice { get; set; }

        public virtual ICollection<UpdateOrderItemDto> OrderItems { get; set; }
    }
}
