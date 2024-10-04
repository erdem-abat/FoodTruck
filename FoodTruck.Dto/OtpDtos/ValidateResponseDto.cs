namespace FoodTruck.Dto.OtpDtos
{
    public class ValidateResponseDto
    {
        bool response = false;

        public bool Response
        {
            get { return response; }
            set { response = value; }
        }
    }
}
