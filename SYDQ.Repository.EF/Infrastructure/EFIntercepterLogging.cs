using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;

namespace SYDQ.Repository.EF
{
    public class EfIntercepterLogging : DbCommandInterceptor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();

            ShowDetails("Scalar", command.CommandText, _stopwatch.ElapsedMilliseconds, interceptionContext.Exception);

            base.ScalarExecuted(command, interceptionContext);
        }
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();

            ShowDetails("NonQuery", command.CommandText, _stopwatch.ElapsedMilliseconds, interceptionContext.Exception);

            base.NonQueryExecuted(command, interceptionContext);
        }
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
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
                EFDebuger(EfIntercepterLogType.Info, infoMsg);
            }
            else
            {
                string errorMsg = string.Format("\r\n-----------------Command:{3}\r\nException:{4}\r\n<--Executed {0} Command Error in {1} milliseconds ended at {2}-->\r\n",
                    commandType, (int)milliseconds, DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), commandText, ex);
                EFDebuger(EfIntercepterLogType.Error, errorMsg);
            }
        }

        private void EFDebuger(EfIntercepterLogType type, string errorMsg)
        {
            Trace.TraceInformation(errorMsg);
            //if (type == EFIntercepterLogType.Info)
            //    CommonHelper.LogWriter(LogWriterType.EFIntercepterLogging_Info, errorMsg);
            //if (type == EFIntercepterLogType.Error)
            //    CommonHelper.LogWriter(LogWriterType.EFIntercepterLogging_Error, errorMsg);
        }
    }

    public enum EfIntercepterLogType
    {
        Info,
        Error
    }
}
