namespace WebApi.Extensions
{
    public static class InvitationKeyStringExtensions
    {

        /// <summary>
        /// Checks if invitation key's length equals to Constants.INV_KEY_LENGTH
        /// </summary>
        public static bool IsInvKeyValid(this string invitationKey)
        {
            return invitationKey.Length == Constants.INV_KEY_LENGTH;
        }
    }
}
