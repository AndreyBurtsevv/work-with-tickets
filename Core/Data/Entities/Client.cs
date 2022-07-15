using System.Collections.Generic;

namespace BoxOffice.Core.Data.Entities
{
    public class Client
    {
        public Client()
        {
            Tickets = new List<Ticket>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }

        public IList<Ticket> Tickets { get; set; }
    }
}
