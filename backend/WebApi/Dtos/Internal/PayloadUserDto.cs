using WebApi.Models.Internal;

namespace WebApi.Dtos.Internal
{

    /// <summary>
    /// This is duplicate of standard User model, but without attributes.
    /// In this model, password property will not be skipped by Newtonsoft.Json parser.
    /// It is useful for grabbing password from user payload in authentication endpoint.
    /// </summary>
    public class PayloadUserDto : IUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

}
