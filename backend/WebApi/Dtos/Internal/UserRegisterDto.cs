namespace WebApi.Dtos.Internal
{
    public class UserRegisterDto
    {
        public UserDto User { get; set; }
        public string InvitationKey { get; set; }
        public string ReCaptchaToken { get; set; }
    }
}
