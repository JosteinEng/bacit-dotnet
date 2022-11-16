using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Repositories
{
    // This is the Repository for users.
    // The repository class functions as the main connection to the Db related to CRUD actions.
    // Most of the methods defined in the Repository are used by the Controllers.
    public class EFUserRepository : UserRepositoryBase, IUserRepository
    {
        // Field variable for the DbContext(dataContext) obj
        private readonly DataContext dataContext;

        public EFUserRepository(DataContext dataContext, UserManager<IdentityUser> userManager) : base(userManager)
        {
            this.dataContext = dataContext;
        }

        // Method deletes/drops a row in the Db based on the matching string value.
        public void Delete(string email)
        {
            UserEntity? user = GetUserByEmail(email);
            //var aspNetUsersEntity = GetUserByNetMail(email)
            if (user == null)
                return;
            dataContext.Users.Remove(user);
            dataContext.SaveChanges();
        }

        // Method fetches User values based on a string value.
        private UserEntity? GetUserByEmail(string email)
        {
            return dataContext.Users/*.Include(x => x.AspNetUsers)*/.FirstOrDefault(x => x.Email == email);
        }

        // Method fetches all User entries in the Db.
        public UserEntity[] GetUsers()
        {
            return dataContext.Users.ToArray();
        }

        // Method adds the obj values into the Db
        public void Add(UserEntity user)
        {
            var existingUser = GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists found");
            }
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
        }
        
        // Method updates the obj values in the the Db
        public void Update(UserEntity user, List<string> roles)
        {
            var existingUser = GetUserByEmail(user.Email);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            existingUser.Email = user.Email;
            existingUser.EmployeeNumber = user.EmployeeNumber;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Role = user.Role;
            existingUser.Team = user.Team;

            dataContext.SaveChanges();
            SetRoles(user.Email, roles);
        }

        // This method checks if a user has been set as the connected team leader in a team.
        public bool IsUserInUseTeam(string email)
        {
            return dataContext.Teams.Any(x => x.User.Email == email);
        }

        // This method checks if a user has been set as a responsible employee in a suggestion.
        public bool IsUserInUseSuggestionUser(string email)
        {
            return dataContext.Suggestions.Any(x => x.User.Email == email);
        }
        
        // This method checks if a user has been set as the author of a suggestion.
        public bool IsUserInUseSuggestionEmployee(string email)
        {
            return dataContext.Suggestions.Any(x => x.Employee.Email == email);
        }
        
        // This method checks if a user has been set as the author of a JustDoIt
        public bool IsUserInUseJustDoIt(string email)
        {
            return dataContext.Justdoit.Any(x => x.Employee.Email == email);
        }
    }
}
