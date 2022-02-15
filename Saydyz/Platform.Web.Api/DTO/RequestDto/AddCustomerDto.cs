namespace Platform.Web.Api.DTO.RequestDto
{
    public class AddCustomerDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public string PhoneNo { get; set; }
        public int CustomerTypeId { get; set; }
    }
}
