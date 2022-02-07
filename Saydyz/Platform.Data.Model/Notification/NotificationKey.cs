using System.ComponentModel.DataAnnotations;

namespace Platform.Data.Model.Notification
{
    public class NotificationKey : BaseEntity
    {
        [Required(ErrorMessage = "Notification key is required")]
        public string Key { get; set; }
        
        //not required at this stage due to initial seed data
        public string Groups { get; set; }
        
        [Required(ErrorMessage = "Notification text is required")]
        public string Text { get; set; }

        public bool? GenerateOnFailure { get; set; }

        public string ErrorText { get; set; }

        public string Module { get; set; }
    }
}