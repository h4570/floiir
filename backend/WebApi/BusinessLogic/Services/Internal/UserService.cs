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

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tries to register user after validation.
        /// </summary>
        /// <param name="payloadUser"></param>
        /// <param name="invKey"></param>
        /// <returns>
        /// 462 - when invitation key was used by another user, 
        /// 463 - when new user object properties are invalid, 
        /// 464 - when given email from new user object is already used, 
        /// 465 - when given login from new user object is already used
        /// </returns>
        public async Task<DataOrStatusCodeDto<IUser>> TryRegister_API(UserDto payloadUser, InvitationKey invKey, ConfigEnvironment config)
        {
            if (invKey.UsedByUserId == null)
                if (payloadUser.IsValid())
                    if (!EmailExists(payloadUser.Email))
                        if (!LoginExists(payloadUser.Login))
                        {
                            var newUser = payloadUser.ToUser();
                            newUser.Password = AuthUtilities.ComputeSha256Hash(payloadUser.Password, config.Salt);
                            payloadUser.Password = null;
                            await _context.Users.AddAsync(newUser);
                            await _context.SaveChangesAsync();
                            invKey.UsedByUserId = newUser.Id;
                            await _context.SaveChangesAsync();
                            return new DataOrStatusCodeDto<IUser>(payloadUser);
                        }
                        else return new DataOrStatusCodeDto<IUser>(465, "There is already user with this login.");
                    else return new DataOrStatusCodeDto<IUser>(464, "There is already user with this email.");
                else return new DataOrStatusCodeDto<IUser>(463, "User properties validation failed.");
            else return new DataOrStatusCodeDto<IUser>(462, "Given invitation key was used.");
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
