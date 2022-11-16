using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bacit_dotnet.MVC.Models
{
    // This is the model for Teams. Model is the M in MVC
    // The model is like a reflection of the related Db table, in this case the Teams Db table.
    // Each variable in the model reflects each column in the Db table.
    
    // Using [DataAnnotations] we can set up client-side validation for each variable/column before sending sending the data to the Db.
    // Even though many of the [DataAnnotations] keywords are useful when using db migrations, we have mainly done all Db edits manually.
    
    // [Key] defines the primary key of the table.
    // [Required] defines that the variable can not be null. By adding (ErrorMessage="") we can also define a custom error message.
    // [Range] defines that the int value must be a value between 1 and ints max possible value.
    // [StringLenght] defines that the string lenght has a maximum and minimum lenght. 
    // [Foreign Key] defines that the variable is a foreign key connected to another table.
    public class Teams
    {
        [Key]
        public int TeamId { get; set; }

        [Column("team_name")]
        [Required(ErrorMessage = "Vennligst fyll inn et team navn.")]
        [StringLength(50, MinimumLength = 1)]
        public string TeamName { get; set; }

        [ForeignKey("userID")]
        [Required(ErrorMessage = "Vennligst velg en ansatt.")]        
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}