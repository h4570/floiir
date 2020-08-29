using System.ComponentModel.DataAnnotations;
using WebApi.Models.Internal;

namespace WebApi.Dtos.Internal
{
    public class LoginSuccessResponseDto
    {

        public LoginSuccessResponseDto(User user, string jwtToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Login = user.Login;
            Token = jwtToken;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

    }
}
