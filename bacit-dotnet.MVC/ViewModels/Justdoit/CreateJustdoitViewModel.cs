using System.ComponentModel.DataAnnotations;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bacit_dotnet.MVC.ViewModels
{
    public class CreateJustdoitViewModel
    {
        public IFormFile? Attachments { get; set; }

        public IFormFile? AttachmentsAfter { get; set; }
        
        [ValidateNever]
        public Teams[] Teams { get; set; }
        
        [ValidateNever]
        public UserEntity[] Users { get; set; }
        
        [Required(ErrorMessage = "Vennligst velg en ansatt.")]
        [Range(1, int.MaxValue)]
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Vennligst fyll inn tittelen.")]
        [StringLength(30,MinimumLength = 2)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Vennligst fyll inn beskrivelsen.")]
        [StringLength(500, MinimumLength = 2)]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Vennligst velg en kategori.")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Vennligst velg et gyldig team.")]
        [Range(1, int.MaxValue)]
        public int TeamId { get; set; }
    }
}
