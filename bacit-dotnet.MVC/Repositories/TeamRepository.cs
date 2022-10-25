using bacit_dotnet.MVC.Data;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Users objTeam)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int teamId)
        {
            throw new NotImplementedException();
        }

        public Teams[] GetAllTeams()
        {
            throw new NotImplementedException();
        }

        public Teams? GetTeamByTeamId(int teamId)
        {
            throw new NotImplementedException();
        }

        public int Update(Users objTeam)
        {
            throw new NotImplementedException();
        }
    }
}
