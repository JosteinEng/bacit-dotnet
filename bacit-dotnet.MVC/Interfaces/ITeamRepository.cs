using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface ITeamRepository
    {
        // returns id ( > 0 for newly added row or 0 if no new row was added )
        public int Add(Users objTeam);

        // returns rows-affect ( > 0 for rows-affected or 0 if no rows were affected )
        public int Update(Users objTeam);

        // returns if action was completed successfully ( true or false )
        public bool Delete(int teamId);

        // returns a UserViewModel obj based on id or null if no category was found
        public Teams? GetTeamByTeamId(int teamId);

        // returns all teams added to the db
        public Teams[] GetAllTeams();
    }
}
