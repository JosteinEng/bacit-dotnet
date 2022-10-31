using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bacit_dotnet.MVC.Models
{
    public class Teams
    {
        [Key]
        public int TeamId { get; set; }

        [Column("team_name")]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string TeamName { get; set; }

        [ForeignKey("userID")]
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
