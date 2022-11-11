using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using Microsoft.EntityFrameworkCore;

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

            // This check is for 'before' Attachments
            if (objJustdoit.Attachments != null && objJustdoit.Attachments.Length > 0)
            {
                justdoitBeforeEdit.Attachments = objJustdoit.Attachments;
            }
            
            // This check is for 'after' Attachments
            if (objJustdoit.AttachmentAfter != null && objJustdoit.AttachmentAfter.Length > 0)
            {
                justdoitBeforeEdit.AttachmentAfter = objJustdoit.AttachmentAfter;
            }

            // TODO: Når user system er på plass. Validere at forslag tilhører brukeren ved endring eller sletting.


            justdoitBeforeEdit.EmployeeId = objJustdoit.EmployeeId;
            justdoitBeforeEdit.Title = objJustdoit.Title;
            justdoitBeforeEdit.Description = objJustdoit.Description;
            justdoitBeforeEdit.Category = objJustdoit.Category;
            justdoitBeforeEdit.TeamId = objJustdoit.TeamId;

            return _context.SaveChanges();
        }

        public Justdoit? GetJustdoitByJustdoitId(int justdoitId)
        {
            return _context.Justdoit.Include(x => x.Team).FirstOrDefault(x => x.JustdoitId == justdoitId);
        }

        public Justdoit[] GetAllJustdoit()
        {
            return _context.Justdoit.Include(x => x.Team).ToArray();
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
