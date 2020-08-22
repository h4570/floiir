namespace WebApi.Extensions
{
    public static class InvitationKeyStringExtensions
    {

        /// <summary>
        /// Checks if invitation key length is valid
        /// </summary>
        public static bool IsInvKeyValid(this string invitationKey)
        {
            return invitationKey.Length == Constants.INV_KEY_LENGTH;
        }
    }
}
