using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities;

[Table("TBL_User")]
public class User
{
    [Key]
    public Guid UserID { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; } = null!;
    public decimal Wallet { get; set; }
    public string IsLocked { get; set; } = "N";
    public int AcountNumber { get; set; }
    public int Pin { get; set; }

}
