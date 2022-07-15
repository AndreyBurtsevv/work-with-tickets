using BoxOffice.Core.Dto;
using BoxOffice.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BoxOffice.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _service;

        public AccountController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("client-registration")]
        public async Task<IActionResult> ClientRegistration(Registration model)
        {
            return Ok(await _service.ClientRegistrationAsync(model));
        }

        [HttpPost("client-login")]
        public async Task<IActionResult> ClientLogin(Login model)
        {
            return Ok(await _service.ClientLogin(model));
        }

        [HttpPost("admin-registration")]
        public async Task<IActionResult> AdminRegistration(Registration model)
        {
            return Ok(await _service.AdminRegistrationAsync(model));
        }

        [HttpPost("admin-login")]
        public async Task<IActionResult> AdminLoginw(Login model)
        {
            return Ok(await _service.AdminLogin(model));
        }
    }
}
