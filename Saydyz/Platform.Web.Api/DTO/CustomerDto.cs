namespace Platform.Web.Api.DTO
{
    public class CustomerDto : BaseDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public virtual AreaDto Area { get; set; }
        public string PhoneNo { get; set; }
        public int CustomerTypeId { get; set; }
    }
}
