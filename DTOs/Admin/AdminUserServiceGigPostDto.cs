using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs.Admin
{
    public class AdminUserServiceGigPostDto
    {
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
        public string Status { get; set; }
    }
}
