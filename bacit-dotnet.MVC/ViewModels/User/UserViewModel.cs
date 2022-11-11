using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;

//FJERNER WARNINGS!
#pragma warning disable
///////////////////////

namespace bacit_dotnet.MVC.ViewModels
{
    public class UserViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string EmployeeNumber { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }
        public List<string> AvailableRoles { get; set; }
        public string ValididationErrorMessage { get; set; }

        public UserEntity[] Users { get; set; }
        public bool IsAdmin { get; set; }

    }
}
