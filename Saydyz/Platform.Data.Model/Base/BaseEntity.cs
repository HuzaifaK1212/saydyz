using System;

namespace Platform.Data.Model
{
    
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
        
    }

}