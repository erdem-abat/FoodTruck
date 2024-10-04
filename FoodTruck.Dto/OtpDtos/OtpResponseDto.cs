namespace FoodTruck.Dto.OtpDtos
{
    public class OtpResponseDto
    {
        string response = string.Empty;
        public bool success = false;
        public string Response
        {
            get { return response; }
            set { response = value; }
        }
    }
}
