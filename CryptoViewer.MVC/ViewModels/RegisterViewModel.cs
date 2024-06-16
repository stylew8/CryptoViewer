namespace CryptoViewer.MVC.ViewModels
{
    public class RegisterViewModel
    {
        public int userId {get; set; }
        public string username { get; set; } = string.Empty; 
        public string password { get; set; } = string.Empty; 
        public string email { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty; 
        public string address { get; set; } = string.Empty;
        public string salt { get; set; } = string.Empty;
    }
    
}
