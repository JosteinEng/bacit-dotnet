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
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime Deadline { get; set; }

        public byte[]? Attachments { get; set; }
    }
}
