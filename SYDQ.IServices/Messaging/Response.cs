namespace SYDQ.IServices.Messaging
{
    public class Response
    {
        public Response()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
