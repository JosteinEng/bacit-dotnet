﻿using System.ComponentModel.DataAnnotations.Schema;

//FJERNER WARNINGS!
#pragma warning disable
///////////////////////

namespace bacit_dotnet.MVC.Models
{
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
