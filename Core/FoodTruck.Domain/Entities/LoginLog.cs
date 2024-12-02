using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities;

public class LoginLog
{
    [Key]
    public int LoginLogId { get; set; }
    public string Email { get; set; }
    public virtual ApplicationUser User { get; set; }
    public string IpAddress { get; set; }
    public DateTime LoginDate { get; set; }
}