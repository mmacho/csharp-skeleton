using Aseme.Shared.Infrastructure.Utils;

namespace Aseme.Shared.Domain.Support
{
    public class PageResult<T>
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public List<T> Data { get; set; }

        public PageResult(List<T> data)
        {
            Data = data;
        }

        internal PageResult(List<T> data = default, int count = 0, int? pageNumber = null, int? pageSize = null)
        {
            int totalPages = PagingUtils.CalculateTotalPages(count, pageSize);

            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalCount = count;
        }

        public static PageResult<T> Page(List<T> data, int count, int? pageNumber, int? pageSize)
        {
            return new(data, count, pageNumber, pageSize);
        }
    }
}