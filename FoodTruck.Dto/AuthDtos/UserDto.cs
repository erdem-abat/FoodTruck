namespace FoodTruck.Dto.AuthDtos
{
    public class UserDto
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}