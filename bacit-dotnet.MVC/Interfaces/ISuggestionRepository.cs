using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Interfaces
{
    public interface ISuggestionRepository
    {
        // returns id ( > 0 for newly added row or 0 if no new row was added )
        public int Add(Suggestions objSuggestion);

        // returns rows-affect ( > 0 for rows-affected or 0 if no rows were affected )
        public int Update(Suggestions objSuggestions);

        // returns if action was completed successfully ( true or false )
        public bool Delete(int suggestionId);

        // returns a SuggestionViewModel obj based on id or null if no suggestion was found
        public Suggestions? GetSuggestionBySuggestionId(int suggestionId);

        // returns a SuggestionViewModel obj based on id or null if no suggestion was found
        public Suggestions? GetSuggestionByEmployeeId(int employeeId);

        // returns all suggestions found in the db
        public Suggestions[] GetAllSuggestions();
    }
}
