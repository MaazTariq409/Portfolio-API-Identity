﻿using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs
{
    public class AdminBlogsDto
    {
        public string title { get; set; }
        public string content { get; set; }
        public string tags { get; set; }
        public string imageUrl { get; set; }
        public string dateCreated { get; set; }
        public string status { get; set; } = "pending";
    }
}
