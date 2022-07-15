using BoxOffice.Core.Data.Entities;
using BoxOffice.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxOffice.Core.Services.Interfaces
{
    public interface ITicketService
    {
        Task<List<TicketDto>> GetAllInSpectacle(int spectacleId);
        Task<FreePlace> GetFreePlaces(int spectacleId);
        Task<List<TicketDto>> GetAllInClient(Client client);
        Task<TicketDto> GetById(int id);
        Task<List<TicketDto>> GetAll();
        Task<TicketDto> BuyAsync(BuyTicket model, Client client);
        Task<string> Refund(int ticketId, Client client);
    }
}