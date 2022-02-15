using AutoMapper;
using Platform.Data.Model.Customer;
using Platform.Data.Model.Flavors;
using Platform.Data.Model.Order;
using Platform.Data.Model.Order.Customer;
using Platform.Web.Api.DTO;
using Platform.Web.Api.DTO.RequestDto;

namespace Platform.Web.Api.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, AddOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<OrderItem, AddOrderItemDto>().ReverseMap();

            CreateMap<Flavor, FlavorDto>().ReverseMap();
            CreateMap<ItemType, ItemTypeDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, AddCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<CustomerType, CustomerTypeDto>().ReverseMap();
            CreateMap<CustomerType, AddCustomerTypeDto>().ReverseMap();

            CreateMap<Area, AreaDto>().ReverseMap();
            CreateMap<Channel, ChannelDto>().ReverseMap();

        }
    }
}
