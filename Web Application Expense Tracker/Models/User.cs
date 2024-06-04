using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Application_Expense_Tracker.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        //public List<Category> Category { get; } = [];

        public ICollection<Transaction> Transaction { get; } = new List<Transaction>();

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
    }
}
