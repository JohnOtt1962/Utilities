using Microsoft.Data.SqlClient;
using System.Data;

namespace Utilities.Logger
{
    public class LoggerBase
    {
        public LoggerBase()
        {

        }

        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error,
            Fatal
        }

        public async Task WriteToLog(string message, Exception ex, LogLevel level, string className, ConnectionDetails conDetails)
        {
            await using SqlConnection con = new SqlConnection(conDetails.ConnectionString);
            await using SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.Parameters.AddRange(GetParameters(level, className, message, ex));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = conDetails.ProcedureName;

                await con.OpenAsync();
                await cmd.ExecuteScalarAsync();
            }
            catch (Exception e)
            {
                //  Do nothing
            }
        }

        private SqlParameter[] GetParameters(LogLevel logLevel, string className, string message, Exception ex)
        {
            return new[]
            {
                new SqlParameter
                {
                    ParameterName = "LogLevel",
                    Value = logLevel
                },
                new SqlParameter
                {
                    ParameterName = "Logger",
                    Value = className
                },
                new SqlParameter
                {
                    ParameterName = "LogMessage",
                    Value = message
                },
                new SqlParameter
                {
                    ParameterName = "StackTrace",
                    Value = GetStackTrace(ex)
                },
                new SqlParameter
                {
                    ParameterName = "InnerException",
                    Value = GetInnerException(ex)
                }
            };
        }

        private string GetStackTrace(Exception ex)
        {
            string retVal = string.Empty;
            if (ex != null && ex.StackTrace != null)
                retVal = ex.StackTrace;

            return retVal;
        }

        private string GetInnerException(Exception ex)
        {
            string retVal = string.Empty;
            while (ex != null)
            {
                retVal += ex.Message;
                ex = ex.InnerException;
            }

            return retVal;
        }
    }
}
