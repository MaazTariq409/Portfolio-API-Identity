using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class UserBlogs
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Blogs")]
        public string title { get; set; }
        public string content { get; set; }
        public string tags { get; set; }
        public string imageUrl { get; set; }
        public string dateCreated { get; set; }
        public string status { get; set; } = "pending";



        [ForeignKey("UserProfileID")]
        [ValidateNever]
        public UserProfile UserProfile { get; set; }
        public int UserProfileID { get; set; }


    }
}
