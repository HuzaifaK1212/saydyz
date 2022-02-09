namespace Platform.Web.Api.DTO
{
    public class FlavorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Price { get; set; }
        public int ItemTypeId { get; set; }
        public virtual ItemTypeDto ItemType { get; set; }
    }
}
