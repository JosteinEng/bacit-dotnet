using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bacit_dotnet.MVC.Models
{
    [Table("usernew")]
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public int EmployeeId { get; set; }

        public string F_Name { get; set; }

        public string L_Name { get; set; }

        public bool Active { get; set; }


        //[Key]
        //public int userId { get; set; }

        //[Required]
        //public int employeeId { get; set; }

        //[Required]
        //[StringLength(255, MinimumLength = 1, ErrorMessage = "Navn må være mer enn 1 karakter og mindre enn 255 karakterer!")]
        //[DisplayName("Fornavn")]
        //public string f_name { get; set; }

        //[Required]
        //[StringLength(255, MinimumLength = 1, ErrorMessage = "Navn må være mer enn 1 karakter og mindre enn 255 karakterer!")]
        //[DisplayName("Etternavn")]
        //public string l_name { get; set; }

        //[Required]
        //[DefaultValue(true)]
        //public bool active { get; set; }

        //[DefaultValue(false)]
        //public bool admin { get; set; }
    }
}
