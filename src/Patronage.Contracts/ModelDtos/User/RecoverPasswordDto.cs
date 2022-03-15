namespace Patronage.Contracts.ModelDtos.User
{
    public class RecoverPasswordDto
    {
        public PropInfo<string>? Email { get; set; }
        public PropInfo<string>? Username { get; set; }
    }
}
