using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class UserServiceGig
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string DateCreated { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public string Status { get; set; } = "pending";


        [ForeignKey("UserProfileID")]
        [ValidateNever]
        public UserProfile UserProfile { get; set; }
        public int UserProfileID { get; set; }


        [ForeignKey("ServiceId")]
        [ValidateNever]
        public UserServices services { get; set; }
        public int ServiceId { get; set; }
    }
}
