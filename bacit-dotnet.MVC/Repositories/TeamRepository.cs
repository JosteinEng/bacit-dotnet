using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext context)
        {
            _context = context;
        }

        public int Add(Teams objTeam)
        {
            var existingTeam = GetTeamAndUserByTeamId(objTeam.TeamId);
            if (existingTeam != null)
            {
                return 0;
            }

            _context.Teams.Add(objTeam);
            _context.SaveChanges();

            return objTeam.TeamId;
        }

        public int Update(Teams objTeam)
        {
            var teamBeforeEdit = GetTeamAndUserByTeamId(objTeam.TeamId);
            if (teamBeforeEdit == null)
            {
                return 0;
            }

            teamBeforeEdit.TeamName = objTeam.TeamName;
            teamBeforeEdit.UserId = objTeam.UserId;

            return _context.SaveChanges();
        }

        public Teams? GetTeamAndUserByTeamId(int teamId)
        {
            return _context.Teams.Include(x => x.User).FirstOrDefault(x => x.TeamId == teamId);
        }

        public Teams[] GetAllTeamsAndUsers()
        {
            return _context.Teams.Include(x => x.User).ToArray();
        }

        public bool ForeignKeyInUse(int teamId)
        {
            return _context.Suggestions.Any(x => x.TeamId == teamId);
        }
        
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
    }
}