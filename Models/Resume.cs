using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.Models
{
    public class Resume
    {
        [Key]
        public int Id { get; set; }
        public string CV { get; set; }
    }
}
