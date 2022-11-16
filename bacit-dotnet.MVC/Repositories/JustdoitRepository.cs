using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Repositories
{
    // This is the Repository for Justdoit suggestions.
    // The repository class functions as the main connection to the Db related to CRUD actions.
    // Most of the methods defined in the Repository are used by the Controllers.
    public class JustdoitRepository : IJustdoitRepository
    {
        // Field variable for the DbContext obj
        private readonly DataContext _context;

        public JustdoitRepository(DataContext context)
        {
            _context = context;
        }

        // Method adds the obj values into the Db
        public int Add(Justdoit objJustdoit)
        {
            // If statement checks if the suggestion value is already in use in the Db.
            var existingJustdoit = GetJustdoitByJustdoitId(objJustdoit.JustdoitId);
            if (existingJustdoit != null)
            {
                return 0;
            }
                
            _context.Justdoit.Add(objJustdoit);
            _context.SaveChanges();

            return objJustdoit.JustdoitId;
        }

        // Method updates the obj values in the the Db
        public int Update(Justdoit objJustdoit)
        {
            // The if statement checks if the fetched value is empty(null), meaning the row to update is empty.
            var justdoitBeforeEdit = GetJustdoitByJustdoitId(objJustdoit.JustdoitId);
            if (justdoitBeforeEdit == null)
            {
                return 0;
            }

            // This check is for 'before' Attachments
            // The if statement checks if the Justdoit obj contains an attachment.
            // If attachment contains a value, we update the Db row with the new attachment data.
            // If not, make no changes to the existing attachment in the Db row.
            if (objJustdoit.Attachments != null && objJustdoit.Attachments.Length > 0)
            {
                justdoitBeforeEdit.Attachments = objJustdoit.Attachments;
            }
            
            // This check is for 'after' Attachments.
            // The if statement checks if the Justdoit obj contains an attachmentAfter.
            // If attachment contains a value, we update the Db row with the new attachment data.
            // If not, make no changes to the existing attachment in the Db row.
            if (objJustdoit.AttachmentAfter != null && objJustdoit.AttachmentAfter.Length > 0)
            {
                justdoitBeforeEdit.AttachmentAfter = objJustdoit.AttachmentAfter;
            }

            justdoitBeforeEdit.EmployeeId = objJustdoit.EmployeeId;
            justdoitBeforeEdit.Title = objJustdoit.Title;
            justdoitBeforeEdit.Description = objJustdoit.Description;
            justdoitBeforeEdit.Category = objJustdoit.Category;
            justdoitBeforeEdit.TeamId = objJustdoit.TeamId;

            return _context.SaveChanges();
        }

        // Method fetches Justdoit values based on an Id value.
        // .Include fetches related Team and User(Employee) data.
        public Justdoit? GetJustdoitByJustdoitId(int justdoitId)
        {
            return _context.Justdoit.Include(x => x.Team).Include(x => x.Employee).FirstOrDefault(x => x.JustdoitId == justdoitId);
        }

        // Method fetches all Justdoit entries in the Db.
        // .Include fetches related Team and User(Employee) data.
        public Justdoit[] GetAllJustdoit()
        {
            return _context.Justdoit.Include(x => x.Team).Include(x => x.Employee).ToArray();
        }

        // This method is not in use yet.
        public Justdoit? GetJustdoitByEmployeeId(int employeeId)
        {
            throw new NotImplementedException();
        }

        // Method deletes/drops a row in the Db based on the matching id value.
        // Returns true or false based on the output of the action.
        public bool Delete(int justdoitId)
        {
            var justdoitToDelete = GetJustdoitByJustdoitId(justdoitId);

            if (justdoitToDelete == null)
            {
                return false;
            }

            _context.Justdoit.Remove(justdoitToDelete);

            var rowsAffected = _context.SaveChanges();
            var isSuccessful = rowsAffected > 0;

            return isSuccessful;
        }
    }
}
