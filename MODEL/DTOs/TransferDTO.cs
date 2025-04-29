using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MODEL.DTOs;

public class TransferDTO
{
    public string? senderName { get; set; }
    public string? receiverName { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime TransferDate { get; set; }
}
