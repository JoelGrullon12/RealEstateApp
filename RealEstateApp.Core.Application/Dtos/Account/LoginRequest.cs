namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class LoginRequest
    {
        public string UserOrEmail { get; set; }
        public string Password { get; set; }
    }
}