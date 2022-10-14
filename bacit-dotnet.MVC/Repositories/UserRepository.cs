using bacit_dotnet.MVC.Data;
using bacit_dotnet.MVC.Data.Migrations;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Users objUser)
        {
            var exsitingUser = GetUserByEmployeeId(objUser.EmployeeId);
            if (exsitingUser != null)
            {
                return 0;
            }

            _context.Users.Add(objUser);
            
            _context.SaveChanges();

            return objUser.UserId;
        }

        public int Update(Users objUser)
        {
            var userBeforeEdit = GetUserByEmployeeId(objUser.EmployeeId);
            if (userBeforeEdit == null)
            {
                return 0;
            }

            userBeforeEdit.F_Name = objUser.F_Name;
            userBeforeEdit.L_Name = objUser.L_Name;
            userBeforeEdit.Active = objUser.Active;

            //_context.Users.Update(objUser);

            return _context.SaveChanges();
        }

        // for admin
        public Users? GetUserByUserId(int userId)
        {
            return _context.Users.Find(userId);
        }

        // for users
        public Users? GetUserByEmployeeId(int employeeId)
        {
            return _context.Users.SingleOrDefault(u => u.EmployeeId == employeeId);
        }

        public Users[] GetAllUsers()
        {
            return _context.Users.ToArray();
        }


        public bool Delete(int userId)
        {
            var userToDelete = GetUserByUserId(userId);

            if (userToDelete == null)
            {
                return false;
            }

            _context.Users.Remove(userToDelete);

            var rowsAffected = _context.SaveChanges();
            var isSuccessful = rowsAffected > 0;
            
            return isSuccessful;
        }
    }
}
