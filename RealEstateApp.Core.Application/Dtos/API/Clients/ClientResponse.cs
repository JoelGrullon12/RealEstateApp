namespace RealEstateApp.Core.Application.Dtos.API.Clients
{
    public class ClientResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PropertiesCount { get; set; }
    }
}