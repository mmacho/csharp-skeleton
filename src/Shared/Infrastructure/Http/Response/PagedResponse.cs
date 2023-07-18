namespace Aseme.Shared.Infrastructure.Http.Response
{
    public class PagedResponse<T> : BaseResponse
    {
        public List<T> Data { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        // HATEOAS 
        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public Uri? FirstPage { get; set; }

        public Uri? LastPage { get; set; }

        public Uri? NextPage { get; set; }

        public Uri? PreviousPage { get; set; }

        public PagedResponse()
        {

        }

        public PagedResponse(List<T> data)
        {
            Data = data;
        }


        internal PagedResponse(bool succeeded, List<T> data = default, int count = 0, int? pageNumber = null, int? pageSize = null)
        {
            Data = data;
            PageNumber = pageNumber;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public static PagedResponse<T> Success(List<T> data, int count, int? pageNumber, int? pageSize)
        {
            return new(true, data, count, pageNumber, pageSize);
        }

    }
}

