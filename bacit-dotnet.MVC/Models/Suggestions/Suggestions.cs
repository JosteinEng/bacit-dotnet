﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace bacit_dotnet.MVC.Models
{
    public class Suggestions
    {
        [Key]
        public int SuggestionId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(30,MinimumLength = 2)]
        public string? Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime Deadline { get; set; }

        public byte[]? Attachments { get; set; }
    }
}
