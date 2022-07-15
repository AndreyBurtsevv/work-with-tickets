using System.Collections.Generic;

namespace BoxOffice.Core.Dto
{
    public class TicketDto
    {
        public int Id { get; set; }

        public int Seat { get; set; }

        public ulong SpectacleStartTime { get; set; }

        public ulong SpectacleEndTime { get; set; }

        public string ClientFullName { get; set; }

        public string SpectacleName { get; set; }
    }

    public class FreePlace
    {
        public FreePlace()
        {
            Seats = new List<int>();
        }
        public IList<int> Seats { get; set; }
    }

    public class BuyTicket
    {
        public int SpectacleId { get; set; }
        public int Seat { get; set; }
    }
}
