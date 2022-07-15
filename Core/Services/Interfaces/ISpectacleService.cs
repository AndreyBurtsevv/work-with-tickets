using BoxOffice.Core.Data.Entities;
using BoxOffice.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxOffice.Core.Services.Interfaces
{
    public interface ISpectacleService
    {
        Task<SpectacleDto> CreateAsync(CreateSpectacle model, Admin admin);
        Task<List<SpectacleDto>> GetAll();
        Task<SpectacleDto> GetById(int id);
        Task<string> RemoveAsync(int id);
        Task<SpectacleDto> UpdateAsync(SpectacleDto model);
    }
}