using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface IUserRepository
    {
        void Update(UserEntity user, List<string> roles);
        
        void Add(UserEntity user);

        void Delete(string email);
        
        // returns all users added to the db
        UserEntity[] GetUsers();
        
        // returns bool for user admin role
        bool IsAdmin(string email);

        public bool IsUserAdmin(string email);
        
        // returns bool for usage of a user in a team
        public bool IsUserInUseTeam(string email);
        
        // returns bool for usage of a user in a suggestion as a responsible user
        public bool IsUserInUseSuggestionUser(string email);
        
        // returns bool for usage of a user in a suggestion as an author
        public bool IsUserInUseSuggestionEmployee(string email);
        
        // returns bool for usage of a user in a justdoit
        public bool IsUserInUseJustDoIt(string email);
    }
}