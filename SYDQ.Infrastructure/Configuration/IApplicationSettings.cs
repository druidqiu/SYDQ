
namespace SYDQ.Infrastructure.Configuration
{
    public interface IApplicationSettings
    {
        string WebRootUrl { get; }
        string LoggerName { get; }
        string NumberOfResultsPerPage { get; }
        string JanrainApiKey { get; }
        string SmtpHost { get; }
        string SmtpUserAddress { get; }
        string SmtpUserName { get; }
        string SmtpUserPwd { get; }
    }
}
