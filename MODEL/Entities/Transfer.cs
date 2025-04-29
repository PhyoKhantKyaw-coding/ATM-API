using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities;

[Table("TBL_Transfer")]
public class Transfer
{
    [Key]
    public Guid TransferID { get; set; }
    public Guid SenderID { get; set; }
    public Guid ReceiverID { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransferDate { get; set; }
    public string? TransferType { get; set; }
    public string? Description { get; set; }
}
