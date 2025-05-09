using MODEL.ApplicationConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs;

public class UserDTO
{
    public string? UserName { get; set; }
    public int AcountNumber { get; set; }
    public string? Password { get; set; }
}
public class UserResponseDTO
{
    public string? Message { get; set; }
    public object? Data { get; set; }
}
public class UserCreateDTO
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public decimal Wallet { get; set; }
    public int Pin { get; set; }
}

public class UserHistoryDTO
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? TransactionType { get; set; }
}