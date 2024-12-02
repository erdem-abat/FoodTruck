using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities;

public class Campaign : DateFields
{
    public int CampaignId { get; set; }

    [MaxLength(50)]
    public string Description { get; set; }
    [Range(0.01, 99.99)]
    public decimal Discount { get; set; }
    public virtual IEnumerable<FoodCampaign> FoodCampaigns { get; set; }
}