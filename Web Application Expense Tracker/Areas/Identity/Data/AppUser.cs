using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using Web_Application_Expense_Tracker.Models;
using Transaction = Web_Application_Expense_Tracker.Models.Transaction;

namespace Web_Application_Expense_Tracker.Areas.Identity.Data;

public class AppUser : IdentityUser
{
    //[Key]
    //public int UserId { get; set; }

    public List<Category> Category { get; set; } = [];

    public ICollection<Transaction> Transaction { get; set; } = new List<Transaction>();

    //[Column(TypeName = "nvarchar(50)")]
    //public string Password { get; set; }
}

