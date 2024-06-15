namespace CryptoViewer.API.Models
{
    public class CryptocurrencyResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string TrackerAction { get; set; }
        public string BorderColor { get; set; }
        public List<LinkResource> Links { get; set; } = new List<LinkResource>();
    }

    public class LinkResource
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
    }

    public class AddCryptocurrencyViewModel
    {
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string TrackerAction { get; set; }
        public string BorderColor { get; set; }
    }
}
