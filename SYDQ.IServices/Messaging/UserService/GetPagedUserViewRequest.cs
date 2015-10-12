namespace SYDQ.IServices.Messaging.UserService
{
    public class GetPagedUserViewRequest : PagedRequest
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
    }
}
