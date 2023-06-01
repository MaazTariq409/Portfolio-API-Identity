using Portfolio_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portfolio_API.DTOs
{
    public class UserServiceGigDto
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


        //For User Details Below
        public string UserName { get; set; }
        public string UserProfileImage { get; set; }
        public string UserLanguage { get; set; }
        public string UserIntroduction { get; set; }

    }
}
