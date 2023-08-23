namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class RegisterResponse
    {
        public string UserId { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}