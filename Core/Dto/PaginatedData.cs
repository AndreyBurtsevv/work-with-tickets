using Sieve.Models;
using Sieve.Services;
using System.Linq;

namespace BoxOffice.Core.Dto
{
    public class PaginatedData<T>
    {
        public int Size { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
        public int Page { get; set; }
        public IQueryable<T> Data { get; set; }

        public PaginatedData(IQueryable<T> data, int size, int perPage, int page)
        {
            //size = data.Count();
            Data = data;
            Page = size <= 0 ? 1 : page;
            PerPage = perPage;
            Size = size;
            Pages = size == 0 ? 0 : (size / perPage) + (size % perPage == 0 ? 0 : 1);
        }

        public PaginatedData(IQueryable<T> data, SieveModel query, SieveProcessor processor)
        {
            var filtered = processor.Apply(query, data, applyPagination: false);
            var result = processor.Apply(query, filtered, applyFiltering: false, applySorting: false);
            Size = filtered.Count();
            Data = result;
            Page = Size <= 0 ? 1 : (query.Page ?? 1);
            PerPage = query.PageSize ?? filtered.Count();
            Pages = Size == 0 ? 0 : (Size / PerPage) + (Size % PerPage == 0 ? 0 : 1);
        }
    }
}
