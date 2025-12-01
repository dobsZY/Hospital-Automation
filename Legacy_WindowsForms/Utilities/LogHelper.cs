using System;
using System.IO;
using System.Text;

namespace HospitalAutomation.Utilities
{
    public static class LogHelper
    {
        private static readonly object _lock = new object();
        private static string _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        public static void Initialize(string logDirectory = null)
        {
            if (!string.IsNullOrWhiteSpace(logDirectory))
                _logDirectory = logDirectory;

            try
            {
                if (!Directory.Exists(_logDirectory))
                    Directory.CreateDirectory(_logDirectory);
            }
            catch
            {
                // ignore
            }
        }

        private static string GetLogPath()
        {
            var fileName = $"hospital-{DateTime.Now:yyyyMMdd}.log";
            return Path.Combine(_logDirectory, fileName);
        }

        public static void Information(string message)
        {
            Write("INFO", message, null);
        }

        public static void Debug(string message)
        {
            Write("DEBUG", message, null);
        }

        public static void Error(string message, Exception ex = null)
        {
            Write("ERROR", message, ex);
        }

        private static void Write(string level, string message, Exception ex)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level}] ");
                sb.Append(message);
                if (ex != null)
                {
                    sb.AppendLine();
                    sb.Append($"Exception: {ex.GetType().FullName}: {ex.Message}");
                    sb.AppendLine();
                    sb.Append(ex.StackTrace);
                }

                var line = sb.ToString();

                lock (_lock)
                {
                    File.AppendAllText(GetLogPath(), line + Environment.NewLine);
                }
            }
            catch
            {
                // Logging must not throw
            }
        }
    }
}