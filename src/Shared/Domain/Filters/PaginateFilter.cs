namespace Aseme.Shared.Domain
{
    public class PaginateFilter
    {
        // Offset
        public int? PageNumber { get; set; }

        // Limit
        public int? PageSize { get; set; }
    }
}
