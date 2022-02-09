namespace Platform.Web.Api.DTO
{
    public class ConfigurationParameterDto : BaseDto
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public int ProfileId { get; set; }
    }
}
