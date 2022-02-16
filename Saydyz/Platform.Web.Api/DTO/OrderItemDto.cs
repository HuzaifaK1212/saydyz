namespace Platform.Web.Api.DTO
{
    public class OrderItemDto : BaseDto
    {
        public string Quantity { get; set; }
        public string Price { get; set; }
        public virtual FlavorDto Flavor { get; set; }
        public bool IsPromo { get; set; }
        public virtual ItemTypeDto ItemType { get; set; }
    }
}
