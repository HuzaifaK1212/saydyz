namespace Platform.Web.Api.DTO.RequestDto
{
    public class AddOrderItemDto
    {
        public string Quantity { get; set; }
        public string Price { get; set; }
        public int FlavorId { get; set; }
        public bool IsPromo { get; set; }
        public string OrderId { get; set; }
    }
}
