using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Internal
{

    public class LoginPasswordDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReCaptchaToken { get; set; }

    }

}
