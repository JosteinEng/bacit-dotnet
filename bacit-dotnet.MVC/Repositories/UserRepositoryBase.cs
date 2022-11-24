using Microsoft.AspNetCore.Identity;

//FJERNER WARNINGS!
#pragma warning disable
///////////////////////

namespace bacit_dotnet.MVC.Repositories
{
    // This is the Repository for users.
    // The repository class functions as the main connection to the Db related to CRUD actions.
    // Most of the methods defined in the Repository are used by the Controllers.
    public abstract class UserRepositoryBase
    {
        UserManager<IdentityUser> userManager;
        public UserRepositoryBase(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // Method checks if a user in admin.
        // Returns a bool based on the action result
        public bool IsAdmin(string email)
        {
            var identity = userManager.Users.FirstOrDefault(x => x.Email == email);
            var existingRoles = userManager.GetRolesAsync(identity).Result;
            return existingRoles.FirstOrDefault(x => x == "Administrator") != null;
        }

        protected void SetRoles(string userEmail, List<string> roles)
        {
            var identity = userManager.Users.FirstOrDefault(x => x.Email == userEmail);
            var existingRoles = userManager.GetRolesAsync(identity).Result;

            //Remove role access before adding new
            foreach (var existingRole in existingRoles)
            {
                var result = userManager.RemoveFromRoleAsync(identity, existingRole).Result;
            }
            foreach (var role in roles)
            {
                if (!userManager.IsInRoleAsync(identity, role).Result)
                {
                    var result = userManager.AddToRoleAsync(identity, role).Result;
                }
            }
        }
        public UserManager<IdentityUser> UserManager { get; }
    }
}
