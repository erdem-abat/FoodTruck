using Microsoft.AspNetCore.SignalR;

namespace FoodTruck.WebApi.Hubs
{
    public class TruckHub : Hub
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TruckHub(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendTruckCount()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7180/api/Statistics/GetTruckCount");
            var value = await responseMessage.Content.ReadAsStringAsync();
            await Clients.All.SendAsync("ReceiveTruckCount", value);
        }
    }
}
