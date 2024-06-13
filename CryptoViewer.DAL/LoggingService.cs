using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.DAL
{
    public class LoggingService : ILoggingService
    {
        public void Log(string message)
        {
            using (var streamWriter = new StreamWriter($"logs{DateTime.Now.ToString("yyyy-MMdd")}.txt", true))
            {
                streamWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}: {message}");
            }
        }
    }
}
