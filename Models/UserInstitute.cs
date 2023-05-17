using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class UserInstitute
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the Institute")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the BranchNo")]
        public string BranchNo { get; set; }
        [Required(ErrorMessage = "Enter the Website Url")]
        public string Website { get; set; }
    }
}
