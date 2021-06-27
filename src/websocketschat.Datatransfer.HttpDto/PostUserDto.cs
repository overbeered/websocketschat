using System.ComponentModel.DataAnnotations;

namespace websocketschat.Datatransfer.HttpDto
{
    public class PostUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Username: {Username}\tPassword: {Password}";
        }
    }
}
