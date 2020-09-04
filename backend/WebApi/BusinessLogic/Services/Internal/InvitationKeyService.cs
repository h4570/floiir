using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos.Internal;
using WebApi.Extensions;
using WebApi.Models.Internal;

namespace WebApi.BusinessLogic.Services.Internal
{
    public class InvitationKeyService
    {

        private readonly AppDbContext _context;

        public InvitationKeyService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tries to register user after validation.
        /// </summary>
        /// <param name="payloadUser"></param>
        /// <param name="invKey"></param>
        /// <returns>
        /// 460 - when invitation key is invalid (Constants.INV_KEY_LENGTH), 
        /// 461 - when invitation key was not found in database, 
        /// </returns>
        public async Task<DataOrStatusCodeDto<InvitationKey>> GetInvitationKeyIfIsRegisterReady_API(string invKey)
        {
            if (invKey.IsInvKeyValid())
            {
                var foundKeyObj = await _context.InvitationKeys.AsQueryable().SingleOrDefaultAsync(c => c.Key == invKey);
                if (foundKeyObj != null)
                    return new DataOrStatusCodeDto<InvitationKey>(foundKeyObj);
                else return new DataOrStatusCodeDto<InvitationKey>(461, "Given invitation key was not found.");
            }
            else return new DataOrStatusCodeDto<InvitationKey>(460, "Given invitation key is invalid.");
        }

    }
}
