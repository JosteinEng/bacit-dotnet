using System.ComponentModel.DataAnnotations;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bacit_dotnet.MVC.ViewModels;

public class EditSuggestionViewModel
{
    [ValidateNever]
    public Teams[] Teams { get; set; }
    
    public int TeamId { get; set; }

    public int SuggestionId { get; set; }
    
    [Range(1, int.MaxValue)]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(30,MinimumLength = 2)]
    public string? Title { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 2)]
    public string? Description { get; set; }

    public DateTime Deadline { get; set; }
    

}