using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.DAL.Models
{
    public class Cryptocurrency : Entity
    {
        public string Name { get; set; } = null!;
        public string LogoPath { get; set; } = null!;
        public string TrackerAction { get; set; } = null!;
        public string BorderColor { get; set; } = null!;
    }
}
