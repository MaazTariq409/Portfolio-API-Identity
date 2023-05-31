using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class UserProductType
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
 
    }
}
