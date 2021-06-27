namespace websocketschat.Web.Helpers.Auth
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int Lifetime { get; set; }
    }
}
