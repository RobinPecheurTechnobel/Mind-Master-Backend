namespace Mind_Master_Backend.DTOs
{
    public class AccountDataTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
    }
}
