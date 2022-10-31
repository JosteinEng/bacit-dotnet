using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bacit_dotnet.MVC.Models
{
    public class UsersTeams
    {
        [Key]
        public int UserTeamId { get; set; }

        [ForeignKey("UserID")]
        public int UserId { get; set; }
        public Users User { get; set; }

        [ForeignKey("TeamsID")]
        public int TeamsId { get; set; }
        public Teams Teams { get; set; }
    }
}
