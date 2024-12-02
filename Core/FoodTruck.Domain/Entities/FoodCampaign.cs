namespace FoodTruck.Domain.Entities;

public class FoodCampaign
{
    public int FoodId { get; set; }
    public virtual Food Food { get; set; }
    public int CampaignId { get; set; }
    public virtual Campaign Campaign { get; set; }
}
