namespace CodePulseAPI.Models.DTO
{
    public class LoggedUser
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
