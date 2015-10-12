namespace SYDQ.Infrastructure.Web.Authentication
{
    public interface IFormsAuthentication
    {
        void SetAuthenticationToken(string token);
        string GetAuthenticationToken();
        void SignOut();
        string LoginUrl { get; }
    } 
}
