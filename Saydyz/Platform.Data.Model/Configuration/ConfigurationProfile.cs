using System;
using System.Collections.Generic;
using Platform.Data.Model;

namespace Platform.Data.Model.Configuration
{
    public partial class ConfigurationProfile : BaseEntity
    {
        public ConfigurationProfile()
        {
            Parameters = new HashSet<ConfigurationParameter>();
        }

        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }

        public virtual ICollection<ConfigurationParameter> Parameters { get; set; }
    }
}
