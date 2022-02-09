using System.Collections.Generic;

namespace Platform.Web.Api.DTO
{
    public class ConfigurationProfileDto : BaseDto
    {
        public ConfigurationProfileDto()
        {
            Parameters = new HashSet<ConfigurationParameterDto>();
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public virtual ICollection<ConfigurationParameterDto> Parameters { get; set; }
    }
}
