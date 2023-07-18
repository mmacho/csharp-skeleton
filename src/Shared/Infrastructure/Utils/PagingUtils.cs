namespace Aseme.Shared.Infrastructure.Utils
{
    public class PagingUtils
    {
        public static int CalculateTotalPages(int count = 0, int? pageSize = null)
        {
            int totalPages;

            if (pageSize.GetValueOrDefault() == 0)
            {
                totalPages = 0;
            }
            else if (count <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                totalPages = (int)Math.Ceiling(count / (double)pageSize);
            }

            return totalPages;
        }
    }
}