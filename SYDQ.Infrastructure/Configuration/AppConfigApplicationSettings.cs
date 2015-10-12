namespace SYDQ.Infrastructure.Configuration
{
    public class AppConfigApplicationSettings : IApplicationSettings
    {
        public string WebRootUrl
        {
            get { return AppConfigReader.GetConfig(AppConst.WebRootUrl); }
        }
        public string LoggerName
        {
            get { return AppConfigReader.GetConfig(AppConst.LoggerName); }
        }
        public string NumberOfResultsPerPage
        {
            get { return AppConfigReader.GetConfig(AppConst.NumberOfResultsPerPage); }
        }
        public string JanrainApiKey
        {
            get { return AppConfigReader.GetConfig(AppConst.JanrainApiKey); }
        }
        public string SmtpHost
        {
            get { return AppConfigReader.GetConfig(AppConst.SmtpHost); }
        }
        public string SmtpUserAddress
        {
            get { return AppConfigReader.GetConfig(AppConst.SmtpUserAddress); }
        }
        public string SmtpUserName
        {
            get { return AppConfigReader.GetConfig(AppConst.SmtpUserName); }
        }
        public string SmtpUserPwd
        {
            get { return AppConfigReader.GetConfig(AppConst.SmtpUserPwd); }
        }
    }
}
