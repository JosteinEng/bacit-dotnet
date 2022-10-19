using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace bacit_dotnet.MVC.Models
{
    [Table("suggestions")]
    public class Suggestions
    {
        [Key]
        public int SuggestionId { get; set; }

        public int EmployeeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; } 

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yy}", ApplyFormatInEditMode = true)] 
        public string Deadline { get; set; }

        public byte[]? Attachments { get; set; }
    }
}
