using SYDQ.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Repository.EF
{
    public class EFIntercepterLogging : DbCommandInterceptor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public override void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ScalarExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();

            ShowDetails("Scalar", command.CommandText, _stopwatch.ElapsedMilliseconds, interceptionContext.Exception);

            base.ScalarExecuted(command, interceptionContext);
        }
        public override void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();

            ShowDetails("NonQuery", command.CommandText, _stopwatch.ElapsedMilliseconds, interceptionContext.Exception);

            base.NonQueryExecuted(command, interceptionContext);
        }
        public override void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ReaderExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();

            ShowDetails("Reader", command.CommandText, _stopwatch.ElapsedMilliseconds, interceptionContext.Exception);

            base.ReaderExecuted(command, interceptionContext);
        }


        private void ShowDetails(string commandType, string commandText, long milliseconds, Exception ex = null)
        {
            if (ex == null)
            {
                string infoMsg = string.Format("\r\n-----------------\r\n{3}\r\n<--Executed {0} Command in {1} milliseconds ended at {2}-->\r\n",
                    commandType, (int)milliseconds, DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), commandText);
                EFDebuger(EFIntercepterLogType.Info, infoMsg);
            }
            else
            {
                string errorMsg = string.Format("\r\n-----------------Command:{3}\r\nException:{4}\r\n<--Executed {0} Command Error in {1} milliseconds ended at {2}-->\r\n",
                    commandType, (int)milliseconds, DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), commandText, ex.ToString());
                EFDebuger(EFIntercepterLogType.Error, errorMsg);
            }
        }

        private void EFDebuger(EFIntercepterLogType type, string errorMsg)
        {
            Trace.TraceInformation(errorMsg);
            //if (type == EFIntercepterLogType.Info)
            //    CommonHelper.LogWriter(LogWriterType.EFIntercepterLogging_Info, errorMsg);
            //if (type == EFIntercepterLogType.Error)
            //    CommonHelper.LogWriter(LogWriterType.EFIntercepterLogging_Error, errorMsg);
        }
    }

    public enum EFIntercepterLogType
    {
        Info,
        Error
    }
}
