using System.Threading.Tasks;
using RealEstateApp.Core.Application.Dtos.Email;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}