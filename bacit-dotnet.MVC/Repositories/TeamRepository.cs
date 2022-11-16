using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Repositories
{
    // This is the Repository for Teams.
    // The repository class functions as the main connection to the Db related to CRUD actions.
    // Most of the methods defined in the Repository are used by the Controllers.
    public class TeamRepository : ITeamRepository
    {
        // Field variable for the DbContext obj
        private readonly DataContext _context;

        public TeamRepository(DataContext context)
        {
            _context = context;
        }

        // Method adds the obj values into the Db
        public int Add(Teams objTeam)
        {
            // If statement checks if the team value is already in use in the Db.
            var existingTeam = GetTeamAndUserByTeamId(objTeam.TeamId);
            if (existingTeam != null)
            {
                return 0;
            }

            _context.Teams.Add(objTeam);
            _context.SaveChanges();

            return objTeam.TeamId;
        }

        // Method updates the obj values in the the Db
        public int Update(Teams objTeam)
        {
            // The if statement checks if the fetched value is empty(null), meaning the row to update is empty.
            var teamBeforeEdit = GetTeamAndUserByTeamId(objTeam.TeamId);
            if (teamBeforeEdit == null)
            {
                return 0;
            }

            teamBeforeEdit.TeamName = objTeam.TeamName;
            teamBeforeEdit.UserId = objTeam.UserId;

            return _context.SaveChanges();
        }

        // Method fetches Team values based on an Id value.
        // .Include fetches related User data.
        public Teams? GetTeamAndUserByTeamId(int teamId)
        {
            return _context.Teams.Include(x => x.User).FirstOrDefault(x => x.TeamId == teamId);
        }

        // Method fetches all Team entries in the Db.
        // .Include fetches related User data.
        public Teams[] GetAllTeamsAndUsers()
        {
            return _context.Teams.Include(x => x.User).ToArray();
        }

        // Method deletes/drops a row in the Db based on the matching id value.
        // Returns true or false based on the output of the action.
        public bool Delete(int teamId)
        {
            var teamToDelete = GetTeamAndUserByTeamId(teamId);

            if(teamToDelete == null)
            {
                return false;
            }

            _context.Teams.Remove(teamToDelete);

            var rowsAffected = _context.SaveChanges();
            var isSuccessful = rowsAffected > 0;

            return isSuccessful;
        }
        
        // This method checks if a team has been set as the connected team for a suggestions.
        public bool IsTeamInUseSuggestion(int teamId)
        {
            return _context.Suggestions.Any(x => x.TeamId == teamId);
        }
        
        // This method checks if a team has been set as the connected team for a Justdoit.
        public bool IsTeamInUseJustdoit(int teamId)
        {
            return _context.Justdoit.Any(x => x.TeamId == teamId);
        }
    }
}