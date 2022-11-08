using System.ComponentModel.DataAnnotations;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bacit_dotnet.MVC.ViewModels

{
    public class CreateSuggestionViewModel
    {
        public Suggestions Suggestion { get; set; }
        public IFormFile? Attachments { get; set; }
        
        [ValidateNever]
        public Teams[] Teams { get; set; }
    }
}