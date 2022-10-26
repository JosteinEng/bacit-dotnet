using bacit_dotnet.MVC.Data;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Teams objTeam)
        {
            var existingTeam = GetTeamByTeamId(objTeam.TeamId);
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
            var teamBeforeEdit = GetTeamByTeamId(objTeam.TeamId);
            if (teamBeforeEdit == null)
            {
                return 0;
            }

            teamBeforeEdit.TeamName = objTeam.TeamName;
            teamBeforeEdit.UserId = objTeam.UserId;

            return _context.SaveChanges();
        }

        public Teams? GetTeamByTeamId(int teamId)
        {
            return _context.Teams.Find(teamId);
        }

        public Teams[] GetAllTeams()
        {
            return _context.Teams.Include(x => x.User).ToArray();
        }

        public bool Delete(int teamId)
        {
            var teamToDelete = GetTeamByTeamId(teamId);

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
