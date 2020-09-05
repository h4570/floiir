using System.Threading.Tasks;
using WebApi.BusinessLogic.Factories.i18n;
using WebApi.BusinessLogic.Services.External;
using WebApi.BusinessLogic.Services.Internal;
using WebApi.Dtos.Internal;
using WebApi.Extensions;
using WebApi.Models.Internal;

namespace WebApi.BusinessLogic.Facades
{
    public class UserFacade
    {

        private readonly UserService _userService;
        private readonly InvitationKeyService _invitationKeyService;
        private readonly EmailService _emailService;
        private readonly ReCaptchaService _reCaptchaService;
        private readonly ConfigEnvironment _config;

        public UserFacade(AppDbContext context, ConfigEnvironment config)
        {
            _userService = new UserService(context);
            _invitationKeyService = new InvitationKeyService(context);
            _emailService = new EmailService(EmailType.BlueGray);
            _reCaptchaService = new ReCaptchaService();
            _config = config;
        }

        /// <summary>
        /// 1. Checks ReCaptcha and all payload properties.
        /// 2. Adds new user in database (with computed password hash)
        /// 3. Sends confirm email email
        /// </summary>
        public async Task<DataOrStatusCodeDto<IUser>> Register(UserRegisterDto payload, I18nFactory i18n, string host)
        {
            var result = await CheckReCaptchaValidateKeyAndUser(payload, host);
            if (result.Succeed)
            {
                var newUser = await _userService.ComputePasswordHashAndAddUser(payload.User.ToUser(), _config);
                await _invitationKeyService.SetInvitationKeyAsUsed(payload.InvitationKey, newUser);
                _emailService.SendConfirmEmailEmail(newUser, i18n);
                result = new DataOrStatusCodeDto<IUser>(payload.User);
            }
            return result;
        }

        /// <summary>
        /// Checks ReCaptcha and all payload properties.
        /// </summary>
        /// <returns>
        /// 
        /// 460 - when invitation key is invalid (Constants.INV_KEY_LENGTH), 
        /// 461 - when invitation key was not found in database, 
        /// 462 - when invitation key was used by another user, 
        /// 
        /// 463 - when new user object properties are invalid, 
        /// 464 - when given email from new user object is already used, 
        /// 465 - when given login from new user object is already used, 
        /// 
        /// 499 - when reCaptcha validation failed, 
        /// </returns>
        private async Task<DataOrStatusCodeDto<IUser>> CheckReCaptchaValidateKeyAndUser(UserRegisterDto payload, string host)
        {
            var reCaptchaSucceed = await _reCaptchaService.IsReCaptchaSucceed(payload.ReCaptchaToken, _config.ReCaptchaSecret, host);
            if (reCaptchaSucceed)
            {
                var keyResult = await _invitationKeyService.ValidateAndReturnObject(payload.InvitationKey);
                if (keyResult.Succeed)
                {
                    payload.User.TrimProperties();
                    return _userService.ValidateBeforeRegister(payload.User);
                }
                else return keyResult.ToFailType<IUser>();
            }
            else return new DataOrStatusCodeDto<IUser>(499, "ReCaptcha failed.");
        }

    }
}
