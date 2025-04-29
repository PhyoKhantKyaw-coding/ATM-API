using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities;

[Table("TBL_Transaction")]
public class Transaction
{
    [Key]
    public Guid TransactionID { get; set; }
    public Guid UserID { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? TransactionType { get; set; }
}
