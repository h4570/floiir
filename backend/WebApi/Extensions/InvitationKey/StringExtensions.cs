namespace WebApi.Extensions.InvitationKey
{
    public static class StringExtensions
    {

        /// <summary>
        /// Checks if invitation key length is valid
        /// </summary>
        public static bool IsValid(this string invitationKey)
        {
            return invitationKey.Length == 10;
        }
    }
}
