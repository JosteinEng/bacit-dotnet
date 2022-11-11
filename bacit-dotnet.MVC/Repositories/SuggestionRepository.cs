using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Repositories
{
    public class SuggestionRepository : ISuggestionRepository
    {

        private readonly DataContext _context;

        public SuggestionRepository(DataContext context)
        {
            _context = context;
        }

        public int Add(Suggestions objSuggestion)
        {
            var existingTeam = GetSuggestionBySuggestionId(objSuggestion.SuggestionId);
            if (existingTeam != null)
            {
                return 0;
            }
            
            _context.Suggestions.Add(objSuggestion);
            _context.SaveChanges();

            return objSuggestion.SuggestionId;
        }

        public int Update(Suggestions objSuggestions)
        {
            var suggestionBeforeEdit = GetSuggestionBySuggestionId(objSuggestions.SuggestionId);
            if (suggestionBeforeEdit == null)
            {
                return 0;
            }

            // This check is for 'before' Attachments
            if (objSuggestions.Attachments != null && objSuggestions.Attachments.Length > 0)
            {
                suggestionBeforeEdit.Attachments = objSuggestions.Attachments;
            }
            
            // This check is for 'after' Attachments
            if (objSuggestions.AttachmentsAfter != null && objSuggestions.AttachmentsAfter.Length > 0)
            {
                suggestionBeforeEdit.AttachmentsAfter = objSuggestions.AttachmentsAfter;
            }
            
            // TODO: Når user system er på plass. Validere at forslag tilhører brukeren ved endring eller sletting.
            
            suggestionBeforeEdit.EmployeeId = objSuggestions.EmployeeId;
            suggestionBeforeEdit.Title = objSuggestions.Title;
            suggestionBeforeEdit.Description = objSuggestions.Description;
            suggestionBeforeEdit.Deadline = objSuggestions.Deadline;
            suggestionBeforeEdit.Status = objSuggestions.Status;
            suggestionBeforeEdit.Category = objSuggestions.Category;
            suggestionBeforeEdit.TeamId = objSuggestions.TeamId;
            suggestionBeforeEdit.UserId = objSuggestions.UserId;

            return _context.SaveChanges();
        }

        public Suggestions? GetSuggestionBySuggestionId(int suggestionId)
        {
            return _context.Suggestions.Include(x => x.Team).Include(x => x.User).FirstOrDefault(x => x.SuggestionId == suggestionId);
        }

        public Suggestions[] GetAllSuggestions()
        {
            
            return _context.Suggestions.Include(x => x.Team).Include(x => x.User).ToArray();
        }

        public Suggestions? GetSuggestionByEmployeeId(int employeeId)
        {
            throw new NotImplementedException();
        }


        public bool Delete(int suggestionId)
        {
            var suggestionToDelete = GetSuggestionBySuggestionId(suggestionId);

            if (suggestionToDelete == null)
            {
                return false;
            }


            // TODO: Når user system er på plass. Validere at forslag tilhører brukeren ved endring eller sletting.


            _context.Suggestions.Remove(suggestionToDelete);

            var rowsAffected = _context.SaveChanges();
            var isSuccessful = rowsAffected > 0;

            return isSuccessful;
        }
    }
}
