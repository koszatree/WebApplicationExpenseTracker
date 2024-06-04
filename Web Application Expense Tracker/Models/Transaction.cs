using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Web_Application_Expense_Tracker.Areas.Identity.Data;

namespace Web_Application_Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public AppUser? User { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a category.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount have to be greater than 0.")]
        public int Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get 
            { 
                return Category == null ? "" : Category.Icon + " " + Category.Title;
            }
        }

        [NotMapped]
        public string? PositiveOrNegativeAmount
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0");
            }
        }
    }
}
