using System;

namespace Platform.Data.Model.Internals
{
    public class ActivityLog
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Path { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Location { get; set; }
    }
}