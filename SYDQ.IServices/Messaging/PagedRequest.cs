using SYDQ.Infrastructure.Pager;
namespace SYDQ.IServices.Messaging
{
    public class PagedRequest : Request
    {
        public PagedRequest()
        {
            PageIndex = 1;
            PageSize = 10;
            SortField = "Id";
            SortOrder = SortOrder.Asc;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
