using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bacit_dotnet.MVC.Models
{
    [Table("teams")]
    public class Teams
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        public string TeamName { get; set; }
        [Required]
        public Users TeamManager { get; set; }
    }
}
