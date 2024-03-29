﻿using System.Collections.Generic;

namespace Platform.Web.Api.DTO
{
    public class OrderDto : BaseDto
    {
        public OrderDto()
        {
            OrderItems = new HashSet<OrderItemDto>();
        }
        public string OrderCode { get; set; }
        public virtual CustomerDto Customer { get; set; }
        public virtual ChannelDto Channel { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string DeliveryCharge { get; set; }
        public string SalePrice { get; set; }

        public virtual ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
