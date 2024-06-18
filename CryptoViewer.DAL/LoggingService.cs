using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.DAL
{
    /// <summary>
    /// Implements the logging service interface to log messages to a text file.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// Logs a message to a text file.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Log(string message)
        {
            // File path format: logs/yyyy-MMdd.txt
            using (var streamWriter = new StreamWriter($"logs{DateTime.Now.ToString("yyyy-MMdd")}.txt", true))
            {
                // Write timestamped message to the log file
                streamWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}: {message}");
            }
        }
    }
}