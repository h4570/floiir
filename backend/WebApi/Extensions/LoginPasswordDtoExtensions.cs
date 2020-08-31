using WebApi.Dtos.Internal;

namespace WebApi.Extensions
{
    public static class LoginPasswordDtoExtensions
    {

        /// <summary>
        /// Trim all properties just to be sure that we will have not mess in database.
        /// http://tonyshowoff.com/articles/should-you-trim-passwords/
        /// </summary>
        public static void TrimProperties(this LoginPasswordDto req)
        {
            if (req.Login != null) req.Login = req.Login.Trim();
            if (req.Password != null) req.Password = req.Password.Trim();
        }

    }
}
