using Sieve.Attributes;

namespace BoxOffice.Core.Dto
{
    public class SpectacleDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public uint TotalTicket { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public ulong StartTime { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public ulong EndTime { get; set; }
    }

    public class CreateSpectacle
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint TotalTicket { get; set; }
        public ulong StartTime { get; set; }
        public ulong EndTime { get; set; }
    }
}
