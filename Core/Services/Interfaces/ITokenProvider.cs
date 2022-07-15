using BoxOffice.Core.Dto;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BoxOffice.Core.Services.Interfaces
{
    public interface ITokenProvider
    {
        Task<Token> CreateTokensAsync(Claim[] claims);
    }
}