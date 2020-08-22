

namespace WebApi.Extensions.User
{
    public static class UserExtensions
    {

        /// <summary>
        /// Checks if login exist and have proper length (max length defined by SQL column restriction)
        /// </summary>
        public static bool IsLoginValid(this Models.Internal.User user)
        {
            return user.Login != null && user.Login.Length >= 5 && user.Login.Length <= 20;
        }

        /// <summary>
        /// Checks if password exist and have proper length (max length defined by SQL column restriction)
        /// </summary>
        public static bool IsPasswordValid(this Models.Internal.User user)
        {
            return user.UserPassword != null && user.UserPassword.Password != null && user.UserPassword.Password.Length >= 6 && user.UserPassword.Password.Length <= 254;
        }

        /// <summary>
        /// Checks if first name exist and have proper length (max length defined by SQL column restriction)
        /// </summary>
        public static bool IsFirstNameValid(this Models.Internal.User user)
        {
            return user.FirstName != null && user.FirstName.Length >= 1 && user.FirstName.Length <= 35;
        }

        /// <summary>
        /// Checks if last name exist and have proper length (max length defined by SQL column restriction)
        /// </summary>
        public static bool IsLastNameValid(this Models.Internal.User user)
        {
            return user.LastName != null && user.LastName.Length >= 1 && user.LastName.Length <= 35;
        }

        /// <summary>
        /// Checks if email exist, have proper length and symbols (max length defined by SQL column restriction)
        /// </summary>
        public static bool IsEmailValid(this Models.Internal.User user)
        {
            var lengthTest = user.Email != null && user.Email.Length >= 6 && user.Email.Length <= 254;
            if (lengthTest)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(user.Email);
                    return addr.Address == user.Email;
                }
                catch { return false; }
            }
            return false;
        }

        /// <summary>
        /// Checks if all necessary properties exist and have proper length (max length defined by SQL columns restrictions)
        /// </summary>
        public static bool IsValid(this Models.Internal.User user)
        {
            return IsLoginValid(user) &&
                IsPasswordValid(user) &&
                IsFirstNameValid(user) &&
                IsLastNameValid(user) &&
                IsEmailValid(user);
        }


        /// <summary>
        /// Trim all properties just to be sure that we will have not mess in database.
        /// http://tonyshowoff.com/articles/should-you-trim-passwords/
        /// </summary>
        public static void TrimProperties(this Models.Internal.User user)
        {
            if (user.FirstName != null) user.FirstName = user.FirstName.Trim();
            if (user.LastName != null) user.LastName = user.LastName.Trim();
            if (user.Login != null) user.Login = user.Login.Trim();
            if (user.UserPassword != null && user.UserPassword.Password != null) user.UserPassword.Password = user.UserPassword.Password.Trim();
            if (user.Email != null) user.Email = user.Email.Trim();
        }

    }
}
