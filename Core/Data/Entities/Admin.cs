using System.Collections.Generic;

namespace BoxOffice.Core.Data.Entities
{
    public class Admin
    {
        public Admin()
        {
            Spectacles = new List<Spectacle>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }

        public IList<Spectacle> Spectacles { get; set; }
    }
}
