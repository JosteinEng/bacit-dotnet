using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface IUserRepository
    {
        void Update(UserEntity user, List<string> roles);
        void Add(UserEntity user);
        UserEntity[] GetUsers();
        void Delete(string email);
        bool IsAdmin(string email);
    }
}