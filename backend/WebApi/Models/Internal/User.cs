using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{

    public interface IUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class User : IUser
    {

        public User() { }

        public User(IUser iUser)
        {
            Id = iUser.Id;
            Login = iUser.Login;
            Password = iUser.Password;
            FirstName = iUser.FirstName;
            LastName = iUser.LastName;
            Email = iUser.Email;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Login { get; set; }
        [Required]
        [JsonIgnore]
        [StringLength(64)]
        public string Password { get; set; }
        [Required]
        [StringLength(35)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(35)]
        public string LastName { get; set; }
        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
