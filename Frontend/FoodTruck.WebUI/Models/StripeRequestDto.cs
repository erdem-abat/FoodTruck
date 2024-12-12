using Newtonsoft.Json;

namespace FoodTruck.WebUI.Models
{
    public class StripeRequestDto
    {
        [JsonProperty("stripeSessionUrl")]
        public string? StripeSessionUrl { get; set; }
       
        [JsonProperty("stripeSessionId")]
        public string? StripeSessionId { get; set; }
        [JsonProperty("approvedUrl")]
        public string ApprovedUrl { get; set; }
        [JsonProperty("cancelUrl")]
        public string CancelUrl { get; set; }
        [JsonProperty("orderHeaderDto")]
        public OrderHeaderDto orderHeaderDto { get; set; }
    }
}