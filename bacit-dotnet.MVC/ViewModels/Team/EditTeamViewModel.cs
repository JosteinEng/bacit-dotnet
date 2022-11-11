using System.ComponentModel.DataAnnotations;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bacit_dotnet.MVC.ViewModels
{
    public class EditTeamViewModel
    {
        [ValidateNever]
        public UserEntity[] Users { get; set; }

        [Required(ErrorMessage = "Vennligst velg en gyldig teamleder.")]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string TeamName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]

        public int TeamId { get; set; }
    }
}