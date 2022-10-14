using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface IUserRepository
    {
        // returns id ( > 0 for newly added row or 0 if no new row was added )
        public int Add(Users objUser);

        // returns rows-affect ( > 0 for rows-affected or 0 if no rows were affected )
        public int Update(Users objUser);

        // returns if action was completed successfully ( true or false )
        public bool Delete(int userId);

        // returns a UserViewModel obj based on id or null if no category was found
        public Users? GetUserByUserId(int userId);

        public Users? GetUserByEmployeeId(int employeeId);

        // returns all categories added to the db
        public Users[] GetAllUsers();

    }
}
