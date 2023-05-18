using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs
{
    public class UserBlogsDto
    {
        public string title { get; set; }
        public string content { get; set; }
        public string tags { get; set; }
        public string imageUrl { get; set; }
        public string dateCreated { get; set; }

        public string ProfileUrl { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string linkedin { get; set; }
        public string Github { get; set; }
    }
}
