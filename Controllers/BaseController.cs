using BoxOffice.Core.Data;
using BoxOffice.Core.Data.Entities;
using BoxOffice.Core.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace BoxOffice.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly AppDbContext _context;

        public BaseController(IHttpContextAccessor accessor, AppDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        protected Admin GetCurrentAdmin()
        {
            if (_accessor.HttpContext.User.FindFirstValue(ClaimTypes.Role) != "Admin")
                throw new AppException("Invalid role.");

            if (!int.TryParse(_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var id))
                throw new AppException("Invalid token data.");

            var admin = _context.Admins.FirstOrDefault(x => x.Id == id);

            if (admin == null)
                throw new AppException("Invalid token data.");

            return admin;
        }

        protected Client GetCurrentClient()
        {
            if (_accessor.HttpContext.User.FindFirstValue(ClaimTypes.Role) != "Client")
                throw new AppException("Invalid role.");

            if (!int.TryParse(_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var id))
                throw new AppException("Invalid token data.");

            var client = _context.Clients.FirstOrDefault(x => x.Id == id);

            if (client == null)
                throw new AppException("Invalid token data.");

            return client;
        }
    }
}
