using bacit_dotnet.MVC.Models;

//using bacit_dotnet.MVC.Models.Teams;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface ITeamRepository
    {
        // returns id ( > 0 for newly added row or 0 if no new row was added )
        public int Add(Teams objTeam);

        // returns rows-affect ( > 0 for rows-affected or 0 if no rows were affected )
        public int Update(Teams objTeam);

        // returns if action was completed successfully ( true or false )
        public bool Delete(int teamId);

        // returns TeamsViewModel obj based on id or null if no team was found
        public Teams? GetTeamAndUserByTeamId(int teamId);

        // returns all teams added to the db
        public Teams[] GetAllTeamsAndUsers();

        // returns bool for usage of a team in a suggestion
        public bool IsTeamInUseSuggestion(int teamId);
        
        // returns bool for usage of a team in a justdoit
        public bool IsTeamInUseJustdoit(int teamId);

    }
}