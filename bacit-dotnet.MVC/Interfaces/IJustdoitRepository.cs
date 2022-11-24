using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface IJustdoitRepository
    {
        // returns id ( > 0 for newly added row or 0 if no new row was added )
        public int Add(Justdoit objJustdoit);

        // returns rows-affect ( > 0 for rows-affected or 0 if no rows were affected )
        public int Update(Justdoit objJustdoit);

        // returns if action was completed successfully ( true or false )
        public bool Delete(int justdoitId);

        // returns a JustdoitViewModel obj based on id or null if no Justdoit was found
        public Justdoit? GetJustdoitByJustdoitId(int justdoitId);

        // returns a JustdoitViewModel obj based on id or null if no Justdoit was found
        public Justdoit? GetJustdoitByEmployeeId(int employeeId);

        // returns all Justdoit found in the db
        public Justdoit[] GetAllJustdoit();
    }
}
