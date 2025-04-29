using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs;

public class LoginResponseDTO
{
    public string? Message { get; set; }
    public object? Data { get; set; }
    public bool Issuccess { get; set; }
    public bool PasswordStatus { get; set; }
    public bool AccountStatus { get; set; }
    public string? Token { get; set; }

}
public class LoginRequestDTO
{
    public string? UserName { get; set; }
    public int AccountNumber { get; set; }
    public string? Password { get; set; }
}