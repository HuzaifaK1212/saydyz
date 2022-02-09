using System;

namespace Platform.Web.Api.DTO
{
    public class BaseDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
    }
}
