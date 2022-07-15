namespace BoxOffice.Core.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public int Seat { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int SpectacleId { get; set; }
        public Spectacle Spectacle { get; set; }
    }
}
