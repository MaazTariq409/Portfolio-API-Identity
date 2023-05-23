﻿using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs.Admin
{
    public class AdminGetExperienceDto
    {
        public int Id { get; set; }
        [Required]
        public string companyName { get; set; }
        [Required]
        public string jobTitle { get; set; }
        public string responsibility { get; set; }
        public string duration { get; set; }
        public string status { get; set; } = "pending";
    }
}
