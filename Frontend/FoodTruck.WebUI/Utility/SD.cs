namespace FoodTruck.WebUI.Utility
{
    public class SD
    {
        public static string APIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForPickup = "ReadyForPickup";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";

        public const int int_Status_Pending = 1;
        public const int int_Status_Approved = 2;
        public const int int_Status_ReadyForPickup = 3;
        public const int int_Status_Completed = 4;
        public const int int_Status_Refunded = 5;
        public const int int_Status_Cancelled = 6;

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
