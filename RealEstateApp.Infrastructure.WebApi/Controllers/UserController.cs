using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.DTO.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(await _accountService.LoginAsync(request));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterDeveloper")]
        public async Task<IActionResult> RegisterWaiter(RegisterRequest request)
        {
            try
            {
                var user = await _accountService.RegisterDeveloperAsync(request);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
        {
            try
            {
                var user = await _accountService.RegisterAdminUserAsync(request);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
