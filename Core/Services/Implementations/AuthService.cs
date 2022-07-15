using AutoMapper;
using BoxOffice.Core.Data;
using BoxOffice.Core.Data.Entities;
using BoxOffice.Core.Dto;
using BoxOffice.Core.Services.Interfaces;
using BoxOffice.Core.Services.Provaiders;
using BoxOffice.Core.Shared;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BoxOffice.Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenProvider _tokenProvider;

        public AuthService(AppDbContext context, IMapper mapper, ITokenProvider tokenProvider)
        {
            _context = context;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
        }

        public async Task<ClientDto> ClientRegistrationAsync(Registration model)
        {
            model.Email = model.Email.ToLower();
            var client = _context.Clients.FirstOrDefault(x => x.Email == model.Email);
            if (client != null)
                throw new AppException("Client already registered.");

            var newClient = _mapper.Map<Client>(model);
            newClient.Hash = PasswordManager.HashPassword(model.Password);
            var result = _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClientDto>(result.Entity);
        }

        public async Task<Token> ClientLogin(Login model)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Email == model.Email);
            if (client == null || !PasswordManager.VerifyPassword(model.Password, client.Hash))
                throw new AppException("Invalid login or password.");
            var claims = new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                            new Claim(ClaimTypes.Email, client.Email),
                            new Claim(ClaimTypes.Role, "Client"),
                        };
            return await _tokenProvider.CreateTokensAsync(claims);
        }

        public async Task<AdminDto> AdminRegistrationAsync(Registration model)
        {
            model.Email = model.Email.ToLower();
            var admin = _context.Admins.FirstOrDefault(x => x.Email == model.Email);
            if (admin != null)
                throw new AppException("Admin already registered.");

            var newAdmin = _mapper.Map<Admin>(model);
            newAdmin.Hash = PasswordManager.HashPassword(model.Password);
            var result = _context.Admins.Add(newAdmin);
            await _context.SaveChangesAsync();

            return _mapper.Map<AdminDto>(result.Entity);
        }

        public async Task<Token> AdminLogin(Login model)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.Email == model.Email);
            if (admin == null || !PasswordManager.VerifyPassword(model.Password, admin.Hash))
                throw new AppException("Invalid login or password.");
            var claims = new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                            new Claim(ClaimTypes.Email, admin.Email),
                            new Claim(ClaimTypes.Role, "Admin"),
                        };
            return await _tokenProvider.CreateTokensAsync(claims);
        }
    }
}
