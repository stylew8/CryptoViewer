namespace CryptoViewer.MVC.Models.Dto
{
    public class LoginToApiDto
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public LoginToApiResultDto Result { get; set; }
    }
}
