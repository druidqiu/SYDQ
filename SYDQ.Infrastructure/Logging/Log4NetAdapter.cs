using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Logging
{
    public class Log4NetAdapter : ILogger
    {
        private readonly ILog _log;

        public Log4NetAdapter()
        {
            XmlConfigurator.Configure(GetUri());
            _log = LogManager.GetLogger("Logging");
        }

        private Uri GetUri()
        {
            return new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "bin\\Configuration\\log4netConfig.xml");
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void Debug(object message, Exception ex)
        {
            _log.Debug(message, ex);
        }

        public void Info(object message, Exception ex)
        {
            _log.Info(message, ex);
        }

        public void Warn(object message, Exception ex)
        {
            _log.Warn(message, ex);
        }

        public void Error(object message, Exception ex)
        {
            _log.Error(message, ex);
        }

        public void Fatal(object message, Exception ex)
        {
            _log.Fatal(message, ex);
        }
    }
}
