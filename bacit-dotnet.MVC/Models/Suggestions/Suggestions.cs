using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace bacit_dotnet.MVC.Models
{
    // This is the model for Suggestions. Model is the M in MVC
    // The model is like a reflection of the related Db table, in this case the Suggestions Db table.
    // Each variable in the model reflects each column in the Db table.
    
    // Using [DataAnnotations] we can set up client-side validation for each variable/column before sending sending the data to the Db.
    // Even though many of the [DataAnnotations] keywords are useful when using db migrations, we have mainly done all Db edits manually.
    
    // [Key] defines the primary key of the table.
    // [Required] defines that the variable can not be null. By adding (ErrorMessage="") we can also define a custom error message.
    // [Range] defines that the int value must be a value between 1 and ints max possible value.
    // [StringLenght] defines that the string lenght has a maximum and minimum lenght. 
    // [Foreign Key] defines that the variable is a foreign key connected to another table.
    public class Suggestions
    {
        [Key]
        public int SuggestionId { get; set; }
        
        [Required(ErrorMessage = "Vennligst velg en ansatt.")]
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Vennligst fyll inn tittelen.")]
        [StringLength(30,MinimumLength = 2)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Vennligst fyll inn beskrivelsen.")]
        [StringLength(500, MinimumLength = 2)]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "Vennligst velg en tidsfrist.")]
        public DateTime Deadline { get; set; }
        
        [Required(ErrorMessage = "Vennligst velg en status.")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Vennligst velg en PDSA-fase.")]
        public string? Phase { get; set; }

        [Required(ErrorMessage = "Vennligst velg en kategori.")]
        public string? Category { get; set; }

        public byte[]? Attachments { get; set; }
        
        public byte[]? AttachmentsAfter { get; set; }
        
        [ForeignKey("teamId")]
        [Required(ErrorMessage = "Vennligst velg et gyldig team.")]
        [Range(1, int.MaxValue)]
        public int TeamId { get; set; }
        
        // a variable that holds a team obj
        public Teams? Team { get; set; }
        
        [ForeignKey("UserId")]
        [Required(ErrorMessage = "Vennligst velg en ansatt.")]
        public int UserId { get; set; }
        
        // a variable that holds a user obj
        public UserEntity? User { get; set; }
        
        // a variable that holds a user obj
        public UserEntity? Employee { get; set; }
    }
}
