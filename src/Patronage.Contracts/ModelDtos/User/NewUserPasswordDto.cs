namespace Patronage.Contracts.ModelDtos.User
{
    public class NewUserPasswordDto
    {
        public string Id { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}