using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    public class AuthDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class AuthTokenDTO
    {
        public string Token { get; set; }

        public AccountDTO Account { get; set; }
    }
}
