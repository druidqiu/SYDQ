namespace SYDQ.IServices.Messaging
{
    public class PagedRequest : Request
    {
        public PagedRequest()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
