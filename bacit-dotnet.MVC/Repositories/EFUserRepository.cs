﻿using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Repositories
{
    public class EFUserRepository : UserRepositoryBase, IUserRepository
    {
        private readonly DataContext dataContext;

        public EFUserRepository(DataContext dataContext, UserManager<IdentityUser> userManager) : base(userManager)
        {
            this.dataContext = dataContext;
        }

        public void Delete(string email)
        {
            UserEntity? user = GetUserByEmail(email);
            //var aspNetUsersEntity = GetUserByNetMail(email)
            if (user == null)
                return;
            dataContext.Users.Remove(user);
            dataContext.SaveChanges();
        }

        private UserEntity? GetUserByEmail(string email)
        {
            return dataContext.Users/*.Include(x => x.AspNetUsers)*/.FirstOrDefault(x => x.Email == email);
        }

        public UserEntity[] GetUsers()
        {
            return dataContext.Users.ToArray();
        }

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

        // This method checks if a user has been set as the connected team for a suggestions.
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
