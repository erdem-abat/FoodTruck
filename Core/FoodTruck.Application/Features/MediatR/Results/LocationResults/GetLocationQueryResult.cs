namespace FoodTruck.Application.Features.MediatR.Results.LocationResults;

public class GetLocationQueryResult
{
    public int LocationId { get; set; }
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}