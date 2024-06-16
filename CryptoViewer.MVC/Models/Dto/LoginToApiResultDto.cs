namespace CryptoViewer.MVC.Models.Dto
{
    public class LoginToApiResultDto
    {
        public UserToLoginDto user { get; set; }
        public string token { get; set; }
    }
}
