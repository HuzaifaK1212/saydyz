using System;

namespace Platform.Data.Model
{
    public interface IEntity
    {
        int  CreatedBy { get; set; }
        int? UpdatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }

        /**
         * This field is used for Soft Delete
         * */
        bool Active { get; set; }

    }
}