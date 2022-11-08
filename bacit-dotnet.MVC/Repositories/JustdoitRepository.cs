using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Repositories
{
    public class JustdoitRepository : IJustdoitRepository
    {

        private readonly DataContext _context;

        public JustdoitRepository(DataContext context)
        {
            _context = context;
        }

        public int Add(Justdoit objJustdoit)
        {
            _context.Justdoit.Add(objJustdoit);
            _context.SaveChanges();

            return objJustdoit.JustdoitId;
        }

        public int Update(Justdoit objJustdoit)
        {
            var justdoitBeforeEdit = GetJustdoitByJustdoitId(objJustdoit.JustdoitId);
            if (justdoitBeforeEdit == null)
            {
                return 0;
            }


            // TODO: Når user system er på plass. Validere at forslag tilhører brukeren ved endring eller sletting.


            justdoitBeforeEdit.EmployeeId = objJustdoit.EmployeeId;
            justdoitBeforeEdit.Title = objJustdoit.Title;
            justdoitBeforeEdit.Description = objJustdoit.Description;
            justdoitBeforeEdit.Category = objJustdoit.Category;

            return _context.SaveChanges();
        }

        public Justdoit? GetJustdoitByJustdoitId(int justdoitId)
        {
            return _context.Justdoit.Find(justdoitId);
        }

        public Justdoit[] GetAllJustdoit()
        {
            return _context.Justdoit.ToArray();
        }

        public Justdoit? GetJustdoitByEmployeeId(int employeeId)
        {
            throw new NotImplementedException();
        }


        public bool Delete(int justdoitId)
        {
            var justdoitToDelete = GetJustdoitByJustdoitId(justdoitId);

            if (justdoitToDelete == null)
            {
                return false;
            }


            // TODO: Når user system er på plass. Validere at forslag tilhører brukeren ved endring eller sletting.


            _context.Justdoit.Remove(justdoitToDelete);

            var rowsAffected = _context.SaveChanges();
            var isSuccessful = rowsAffected > 0;

            return isSuccessful;
        }
    }
}
