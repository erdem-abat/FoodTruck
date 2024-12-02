namespace FoodTruck.Domain.Entities;

public class FoodRate : DateFields
{
    public int FoodId { get; set; }
    public virtual Food Food { get; set; }

    public int RateId { get; set; }
    public virtual Rate Rate { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}
