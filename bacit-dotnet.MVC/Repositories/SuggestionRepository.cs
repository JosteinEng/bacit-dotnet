using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Repositories
{
    // This is the Repository for Suggestions.
    // The repository class functions as the main connection to the Db related to CRUD actions.
    // Most of the methods defined in the Repository are used by the Controllers.
    public class SuggestionRepository : ISuggestionRepository
    {
        // Field variable for the DbContext obj
        private readonly DataContext _context;

        public SuggestionRepository(DataContext context)
        {
            _context = context;
        }

        // Method adds the obj values into the Db
        public int Add(Suggestions objSuggestion)
        {
            // If statement checks if the suggestion value is already in use in the Db.
            var existingSuggestion = GetSuggestionBySuggestionId(objSuggestion.SuggestionId);
            if (existingSuggestion != null)
            {
                return 0;
            }
            
            _context.Suggestions.Add(objSuggestion);
            _context.SaveChanges();

            return objSuggestion.SuggestionId;
        }

        // Method updates the obj values in the the Db
        public int Update(Suggestions objSuggestions)
        {
            // The if statement checks if the fetched value is empty(null), meaning the row to update is empty.
            var suggestionBeforeEdit = GetSuggestionBySuggestionId(objSuggestions.SuggestionId);
            if (suggestionBeforeEdit == null)
            {
                return 0;
            }

            // This check is for 'before' Attachments.
            // The if statement checks if the Justdoit obj contains an attachment.
            // If attachment contains a value, we update the Db row with the new attachment data.
            // If not, make no changes to the existing attachment in the Db row.
            if (objSuggestions.Attachments != null && objSuggestions.Attachments.Length > 0)
            {
                suggestionBeforeEdit.Attachments = objSuggestions.Attachments;
            }
            
            // This check is for 'after' Attachments.
            // The if statement checks if the Justdoit obj contains an attachmentAfter.
            // If attachment contains a value, we update the Db row with the new attachment data.
            // If not, make no changes to the existing attachment in the Db row.
            if (objSuggestions.AttachmentsAfter != null && objSuggestions.AttachmentsAfter.Length > 0)
            {
                suggestionBeforeEdit.AttachmentsAfter = objSuggestions.AttachmentsAfter;
            }
            
            suggestionBeforeEdit.EmployeeId = objSuggestions.EmployeeId;
            suggestionBeforeEdit.Title = objSuggestions.Title;
            suggestionBeforeEdit.Description = objSuggestions.Description;
            suggestionBeforeEdit.Deadline = objSuggestions.Deadline;
            suggestionBeforeEdit.Status = objSuggestions.Status;
            suggestionBeforeEdit.Phase = objSuggestions.Phase;
            suggestionBeforeEdit.Category = objSuggestions.Category;
            suggestionBeforeEdit.TeamId = objSuggestions.TeamId;
            suggestionBeforeEdit.UserId = objSuggestions.UserId;

            return _context.SaveChanges();
        }

        // Method fetches Suggestion values based on an Id value.
        // .Include fetches related Team and users data.
        public Suggestions? GetSuggestionBySuggestionId(int suggestionId)
        {
            return _context.Suggestions.Include(x => x.Team).Include(x => x.User).Include(x => x.Employee).FirstOrDefault(x => x.SuggestionId == suggestionId);
        }

        // Method fetches all Suggestions entries in the Db.
        // .Include fetches related Team and users data.
        public Suggestions[] GetAllSuggestions()
        {
            
            return _context.Suggestions.Include(x => x.Team).Include(x => x.User).Include(x => x.Employee).ToArray();
        }

        // This method is not in use yet.
        public Suggestions? GetSuggestionByEmployeeId(int employeeId)
        {
            throw new NotImplementedException();
        }
        
        // Method deletes/drops a row in the Db based on the matching id value.
        // Returns true or false based on the output of the action.
        public bool Delete(int suggestionId)
        {
            var suggestionToDelete = GetSuggestionBySuggestionId(suggestionId);

            if (suggestionToDelete == null)
            {
                return false;
            }

            _context.Suggestions.Remove(suggestionToDelete);

            var rowsAffected = _context.SaveChanges();
            var isSuccessful = rowsAffected > 0;

            return isSuccessful;
        }
    }
}
