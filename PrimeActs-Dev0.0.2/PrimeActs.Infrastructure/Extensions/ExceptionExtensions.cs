using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        public static string LogMessage(this Exception ex)
        {
            var logMsg = string.Empty;
            logMsg += Environment.NewLine + "Message :" + ex.Message;
            logMsg += Environment.NewLine + "Source :" + ex.Source;
            logMsg += Environment.NewLine + "Stack Trace :" + ex.StackTrace;
            logMsg += Environment.NewLine + "TargetSite :" + ex.TargetSite;

            if (ex.InnerException != null)
            {
                logMsg += "Inner Exception: " + ex.InnerException.LogMessage();
            }
            return logMsg;
        }
    }
}
