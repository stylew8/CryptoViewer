using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.DAL.Models
{
    public class DbSessionModel
    {
        public Guid DbSessionId { get; set; }

        public string? SessionData { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastAccessed { get; set; }

        public int? UserId { get; set; }
    }
}
