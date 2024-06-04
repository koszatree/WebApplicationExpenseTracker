using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Application_Expense_Tracker.Areas.Identity.Data;

namespace Web_Application_Expense_Tracker.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public List<AppUser> Users { get; } = [];

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Required title.")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        [Required(ErrorMessage = "Required icon.")]
        public string Icon { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; } = "Expense";

        [NotMapped]
        public string? TitleWithIcon
        {
            get { return this.Icon + " " + this.Title; }
        }
    }
}
