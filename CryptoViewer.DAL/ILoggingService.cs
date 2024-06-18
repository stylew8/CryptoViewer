using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.DAL
{
    /// <summary>
    /// Interface for logging services used within the DAL.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        void Log(string message);
    }
}