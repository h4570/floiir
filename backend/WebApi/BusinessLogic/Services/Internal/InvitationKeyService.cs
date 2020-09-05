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
        /// Search for key in db, do couple of validations and return inv key object on success
        /// </summary>
        /// <param name="invKey"></param>
        /// <returns>
        /// 460 - when invitation key is invalid (Constants.INV_KEY_LENGTH), 
        /// 461 - when invitation key was not found in database, 
        /// 462 - when invitation key was used by another user, 
        /// </returns>
        public async Task<DataOrStatusCodeDto<InvitationKey>> ValidateAndReturnObject(string invKey)
        {
            if (invKey.IsInvKeyValid())
            {
                var foundKeyObj = await _context.InvitationKeys.AsQueryable().SingleOrDefaultAsync(c => c.Key == invKey);
                if (foundKeyObj != null)
                    if (foundKeyObj.UsedByUserId == null)
                        return new DataOrStatusCodeDto<InvitationKey>(foundKeyObj);
                    else return new DataOrStatusCodeDto<InvitationKey>(462, "Given invitation key was used.");
                else return new DataOrStatusCodeDto<InvitationKey>(461, "Given invitation key was not found.");
            }
            else return new DataOrStatusCodeDto<InvitationKey>(460, "Given invitation key is invalid.");
        }

        public async Task SetInvitationKeyAsUsed(string invKey, IUser user)
        {
            var obj = await _context.InvitationKeys.AsQueryable().SingleAsync(c => c.Key.Equals(invKey));
            obj.UsedByUserId = user.Id;
            await _context.SaveChangesAsync();
        }

    }
}
