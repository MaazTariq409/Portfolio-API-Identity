using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs
{
    public class AdminProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PermaLink { get; set; }
        public string DateCreated { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string VideoUrl { get; set; }
        public string Status { get; set; } = "pending";
    }
}
