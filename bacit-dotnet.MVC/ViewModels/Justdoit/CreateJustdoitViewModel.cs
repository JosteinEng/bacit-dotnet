using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bacit_dotnet.MVC.ViewModels
{
    public class CreateJustdoitViewModel
    {
        public Models.Justdoit Justdoit { get; set; }
        public IFormFile? Attachments { get; set; }
        
        [ValidateNever]
        public Teams[] Teams { get; set; }
    }
}
