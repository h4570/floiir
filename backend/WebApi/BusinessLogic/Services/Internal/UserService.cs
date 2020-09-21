using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos.Internal;
using WebApi.Extensions;
using WebApi.Misc.Auth;
using WebApi.Models.Internal;

namespace WebApi.BusinessLogic.Services.Internal
{
    public class UserService
    {

        private readonly AppDbContext _context;
        private readonly string _salt;
        private readonly string _privateKey;

        public UserService(AppDbContext context,ConfigEnvironment config)
        {
            _context = context;
            _salt = config.Salt;
            _privateKey = config.PrivateKey;
        }

        /// <summary>
        /// Do couple of validations before entering new user to database.
        /// </summary>
        /// <returns>
        /// 463 - when new user object properties are invalid, 
        /// 464 - when given email from new user object is already used, 
        /// 465 - when given login from new user object is already used
        /// </returns>
        public DataOrStatusCodeDto<IUser> ValidateBeforeRegister(UserDto payloadUser)
        {
            if (payloadUser.IsValid())
                if (!EmailExists(payloadUser.Email))
                    if (!LoginExists(payloadUser.Login))
                        return new DataOrStatusCodeDto<IUser>(payloadUser);
                    else return new DataOrStatusCodeDto<IUser>(465, "There is already user with this login.");
                else return new DataOrStatusCodeDto<IUser>(464, "There is already user with this email.");
            else return new DataOrStatusCodeDto<IUser>(463, "User properties validation failed.");
        }

        public DataOrStatusCodeDto<LoginSuccessResponseDto> Login(LoginPasswordDto payload)
        {
            payload.TrimProperties();
            var hash = AuthUtilities.ComputeSha256Hash(payload.Password, _salt);
            var user = _context.Users.SingleOrDefault(c => c.Login.Equals(payload.Login) && c.Password.Equals(hash));
            if (user != null)
            {
                var jwt = AuthUtilities.GenerateJWTToken(_privateKey, user.Id);
                var res = new LoginSuccessResponseDto(user, jwt);
                return new DataOrStatusCodeDto<LoginSuccessResponseDto>(res);
            }
            else return new DataOrStatusCodeDto<LoginSuccessResponseDto>(461, "User does not exist");
        }

        public async Task<User> ComputePasswordHashAndAddUser(User user, ConfigEnvironment config)
        {
            user.Password = AuthUtilities.ComputeSha256Hash(user.Password, config.Salt);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private bool EmailExists(string email)
        {
            var userWithSameEmail = _context.Users.SingleOrDefault(c => c.Email.ToLower().Equals(email.ToLower()));
            return userWithSameEmail != null;
        }

        private bool LoginExists(string login)
        {
            var userWithSameLogin = _context.Users.SingleOrDefault(c => c.Login.ToLower().Equals(login.ToLower()));
            return userWithSameLogin != null;
        }

    }
}
