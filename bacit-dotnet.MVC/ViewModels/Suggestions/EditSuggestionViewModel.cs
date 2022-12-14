using System.ComponentModel.DataAnnotations;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bacit_dotnet.MVC.ViewModels;

public class EditSuggestionViewModel
{
    public Teams[]? Teams { get; set; }
    
    public UserEntity[]? Users { get; set; }
    
    [Required]
    public int TeamId { get; set; }
    
    [Required]
    public int UserId { get; set; }

    [Range(1, int.MaxValue)]
    public int SuggestionId { get; set; }
    
    [Range(1, int.MaxValue)]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(30,MinimumLength = 2)]
    public string? Title { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 2)]
    public string? Description { get; set; }
    
    [Required]
    public string? Status { get; set; }

    [Required]
    public string? Phase { get; set; }

    [Required]
    public string? Category { get; set; }

    [Required]
    public DateTime Deadline { get; set; }

    public IFormFile? Attachment { get; set; }
    
    public IFormFile? AttchmentsAfter { get; set; }
}