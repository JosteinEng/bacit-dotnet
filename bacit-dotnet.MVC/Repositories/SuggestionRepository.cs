using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;

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


            // TODO: Når user system er på plass. Validere at forslag tilhører brukeren ved endring eller sletting.


            suggestionBeforeEdit.EmployeeId = objSuggestions.EmployeeId;
            suggestionBeforeEdit.Title = objSuggestions.Title;
            suggestionBeforeEdit.Description = objSuggestions.Description;
            suggestionBeforeEdit.Deadline = objSuggestions.Deadline;

            return _context.SaveChanges();
        }

        public Suggestions? GetSuggestionBySuggestionId(int suggestionId)
        {
            return _context.Suggestions.Find(suggestionId);
        }

        public Suggestions[] GetAllSuggestions()
        {
            return _context.Suggestions.ToArray();
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
