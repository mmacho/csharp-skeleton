namespace Aseme.Shared.Infrastructure.Http
{
    public class PaginateFilter
    {
        // Offset
        public int? PageNumber { get; set; }

        // Limit
        public int? PageSize { get; set; }
    }
}
