using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface IJustdoitRepository
    {
        public int Add(Justdoit objJustdoit);

        public int Update(Justdoit objJustdoit);

        public bool Delete(int justdoitId);

        public Justdoit? GetJustdoitByJustdoitId(int justdoitId);

        public Justdoit? GetJustdoitByEmployeeId(int employeeId);

        public Justdoit[] GetAllJustdoit();
    }
}
