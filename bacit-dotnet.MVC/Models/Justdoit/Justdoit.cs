using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace bacit_dotnet.MVC.Models
{
    [Table("justdoit")]
    public class Justdoit
    {
        [Key]
        public int JustdoitId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(30,MinimumLength = 2)]
        public string? Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [Required]
        public string? Category { get; set; }

        public byte[]? Attachments { get; set; }
        
        [ForeignKey("teamId")]
        [Required(ErrorMessage = "Vennligst velg et gyldig team.")]
        [Range(1, int.MaxValue)]
        public int TeamId { get; set; }
        
        public Teams? Team { get; set; }
    }
}
