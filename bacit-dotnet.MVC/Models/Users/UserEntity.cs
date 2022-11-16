using System.ComponentModel.DataAnnotations.Schema;

//FJERNER WARNINGS!
#pragma warning disable
///////////////////////

namespace bacit_dotnet.MVC.Models
{
    // This is the model for User. Model is the M in MVC
    // The model is like a reflection of the related Db table, in this case the Users Db table.
    // Each variable in the model reflects each column in the Db table.
    
    // Using [DataAnnotations] we can set up client-side validation for each variable/column before sending sending the data to the Db.
    // Even though many of the [DataAnnotations] keywords are useful when using db migrations, we have mainly done all Db edits manually.
    // [Table] defines the table name in the Db the model is reflecting.
    [Table("Users")]
    public class UserEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? Team { get; set; }
        public string? Role { get; set; }
    }

}
