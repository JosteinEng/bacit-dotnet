
namespace bacit_dotnet.MVC.ViewModels.Suggestions
{
    public class CreateSuggestionViewModel
    {
        public Models.Suggestions Suggestion { get; set; }
        public IFormFile? Attachments { get; set; }
    }
}
