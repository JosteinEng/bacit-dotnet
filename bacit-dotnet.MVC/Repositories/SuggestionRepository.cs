using bacit_dotnet.MVC.Data;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Repositories
{
    public class SuggestionRepository : ISuggestionRepository
    {

        private readonly ApplicationDbContext _context;

        public SuggestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public int Add(Suggestions objSuggestion)
        {
            _context.Suggestions.Add(objSuggestion);
            _context.SaveChanges();

            return objSuggestion.SuggestionId;
        }

        public bool Delete(int suggestionId)
        {
            throw new NotImplementedException();
        }

        public Suggestions[] GetAllSuggestions()
        {
            return _context.Suggestions.ToArray();
        }

        public Suggestions? GetSuggestionByEmployeeId(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Suggestions? GetSuggestionBySuggestionId(int suggestionId)
        {
            throw new NotImplementedException();
        }

        public int Update(Suggestions objSuggestions)
        {
            throw new NotImplementedException();
        }
    }
}
