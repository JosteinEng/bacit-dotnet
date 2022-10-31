using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.ViewModels

{
    public class CreateSuggestionViewModel
    {
        public Suggestions Suggestion { get; set; }
        public IFormFile? Attachments { get; set; }
    }
}