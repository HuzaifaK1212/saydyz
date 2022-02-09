using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.Data.Model.Notification
{
    public class Notification : BaseEntity
    { 
        public string For { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public string GeneratedBy { get; set; }
        public string Data { get; set; }

        [NotMapped]
        public dynamic UserDepartment { get; set; }
    }
}