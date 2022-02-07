using System;
using System.Collections.Generic;
using Platform.Data.Model;

namespace Platform.Data.Model.Configuration
{
    public partial class ConfigurationParameter : BaseEntity
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public int ProfileId { get; set; }

        public virtual ConfigurationProfile Profile { get; set; }
    }
}
