using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface ISuggestionRepository
    {
        public int Add(Suggestions objSuggestion);

        public int Update(Suggestions objSuggestions);

        public bool Delete(int suggestionId);

        public Suggestions? GetSuggestionBySuggestionId(int suggestionId);

        public Suggestions? GetSuggestionByEmployeeId(int employeeId);

        public Suggestions[] GetAllSuggestions();
    }
}
