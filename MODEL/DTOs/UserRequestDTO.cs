using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs;

public class UserRequestDTO
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public int Pin { get; set; }
}

public class TranferRequestDTO
{
    public int ToAcountNumber { get; set; }
    public Guid FromId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public int Pin { get; set; }
}
public class ChangePinDTO()
{
    public Guid Id { get; set; }
    public int OldPin { get; set; }
    public int NewPin { get; set; }
    public int ConfirmPin { get; set; }
}