using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models.Internal;
using WebApiTests;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace WebApi.Extensions.Tests
{
    [TestClass()]
    public class IUserExtensionsTests
    {

        /// 
        ///  IsLoginValid method
        /// 

        [TestMethod("IsLoginValid_LoginIsNull")]
        public void IsLoginValid_LoginIsNull_ReturnFalse()
        {
            IUser user = new User()
            {
                Login = null
            };

            Assert.IsFalse(user.IsLoginValid());
        }

        [TestMethod("IsLoginValid_LoginIsTooShort")]
        public void IsLoginValid_LoginIsTooShort_ReturnFalse()
        {
            IUser user = new User()
            {
                Login = Utility.RandomStringGenerator(4)
            };

            Assert.IsFalse(user.IsLoginValid());
        }

        [TestMethod("IsLoginValid_LoginIsTooLong")]
        public void IsLoginValid_LoginIsTooLong_ReturnFalse()
        {
            IUser user = new User()
            {
                Login = Utility.RandomStringGenerator(21)
            };

            Assert.IsFalse(user.IsLoginValid());
        }

        [TestMethod("IsLoginValid_GoodLenghtOfLogin")]
        public void IsLoginValid_GoodLenghtOfLogin_ReturnTrue()
        {
            IUser user = new User()
            {
                Login = Utility.RandomStringGenerator(20)
            };

            IUser user2 = new User()
            {
                Login = Utility.RandomStringGenerator(5)
            };

            Assert.IsTrue(user.IsLoginValid() && user2.IsLoginValid());
        }


        /// 
        ///  IsPasswordValid method
        /// 

        [TestMethod("IsPasswordValid_PasswordIsNull")]
        public void IsPasswordValid_PasswordIsNull_ReturnFalse()
        {
            IUser user = new User()
            {
                Password = null
            };

            Assert.IsFalse(user.IsPasswordValid());
        }

        [TestMethod("IsPasswordValid_PasswordIsToShoort")]
        public void IsPasswordValid_PasswordIsToShoort_ReturnFalse()
        {
            IUser user = new User()
            {
                Password = Utility.RandomStringGenerator(5)
            };

            Assert.IsFalse(user.IsPasswordValid());
        }

        [TestMethod("IsPasswordValid_PasswordIsTooLong")]
        public void IsPasswordValid_PasswordIsTooLong_ReturnFalse()
        {
            IUser user = new User()
            {
                Password = Utility.RandomStringGenerator(255)
            };

            Assert.IsFalse(user.IsPasswordValid());
        }

        [TestMethod("IsPasswordValid_PasswordHasGoodLenght")]
        public void IsPasswordValid_PasswordHasGoodLenght_ReturnTrue()
        {
            IUser user = new User()
            {
                Password = Utility.RandomStringGenerator(6)
            };

            IUser user2 = new User()
            {
                Password = Utility.RandomStringGenerator(254)
            };
            Assert.IsTrue(user.IsPasswordValid() && user2.IsPasswordValid());
        }

        /// 
        ///  IsFirstNameValid method
        /// 

        [TestMethod("IsFirstNameValid_FirstNameIsNull")]
        public void IsFirstNameValid_FirstNameIsNull_ReturnFalse()
        {
            IUser user = new User()
            {
                FirstName = null
            };

            Assert.IsFalse(user.IsFirstNameValid());
        }

        [TestMethod("IsFirstNameValid_FirstNameIsToShoort")]
        public void IsFirstNameValid_FirstNameIsToShoort_ReturnFalse()
        {
            IUser user = new User()
            {
                FirstName = ""
            };

            Assert.IsFalse(user.IsFirstNameValid());
        }

        [TestMethod("IsFirstNameValid_FirstNameIsTooLong")]
        public void IsFirstNameValid_FirstNameIsTooLong_ReturnFalse()
        {
            IUser user = new User()
            {
                FirstName = Utility.RandomStringGenerator(36)
            };

            Assert.IsFalse(user.IsFirstNameValid());
        }

        [TestMethod("IsFirstNameValid_FirstNameHasGoodLenght")]
        public void IsFirstNameValid_FirstNameHasGoodLenght_ReturnTrue()
        {
            IUser user = new User()
            {
                FirstName = Utility.RandomStringGenerator(1)
            };

            IUser user2 = new User()
            {
                FirstName = Utility.RandomStringGenerator(35)
            };
            Assert.IsTrue(user.IsFirstNameValid() && user2.IsFirstNameValid());
        }

        /// 
        ///  IsLastNameValid method
        /// 

        [TestMethod("IsLastNameValid_LastNameIsNull")]
        public void IsLastNameValid_LastNameIsNull_ReturnFalse()
        {
            IUser user = new User()
            {
                LastName = null
            };

            Assert.IsFalse(user.IsLastNameValid());
        }

        [TestMethod("IsLastNameValid_LastNameIsToShoort")]
        public void IsLastNameValid_LastNameIsToShoort_ReturnFalse()
        {
            IUser user = new User()
            {
                LastName = ""
            };

            Assert.IsFalse(user.IsLastNameValid());
        }

        [TestMethod("IsLastNameValid_LastNameIsTooLong")]
        public void IsLastNameValid_LastNameIsTooLong_ReturnFalse()
        {
            IUser user = new User()
            {
                LastName = Utility.RandomStringGenerator(36)
            };

            Assert.IsFalse(user.IsLastNameValid());
        }

        [TestMethod("IsLastNameValid_LastNameHasGoodLenght")]
        public void IsLastNameValid_LastNameHasGoodLenght_ReturnTrue()
        {
            IUser user = new User()
            {
                LastName = Utility.RandomStringGenerator(1)
            };

            IUser user2 = new User()
            {
                LastName = Utility.RandomStringGenerator(35)
            };
            Assert.IsTrue(user.IsLastNameValid() && user2.IsLastNameValid());
        }

        /// 
        ///  IsEmailValid method
        /// 

        [TestMethod("IsEmailValid_EmailIsNull")]
        public void IsEmailValid_EmailIsNull_ReturnFalse()
        {
            IUser user = new User()
            {
                Email = null
            };

            Assert.IsFalse(user.IsEmailValid());
        }

        [TestMethod("IsEmailValid_EmailIsToShoort")]
        public void IsEmailValid_EmailIsToShoort_ReturnFalse()
        {
            IUser user = new User()
            {
                Email = "1@d.p"
            };

            Assert.IsFalse(user.IsEmailValid());
        }

        [TestMethod("IsEmailValid_EmailIsTooLong")]
        public void IsEmailValid_EmailIsTooLong_ReturnFalse()
        {
            IUser user = new User()
            {
                Email = Utility.RandomStringGenerator(255)
            };

            Assert.IsFalse(user.IsEmailValid());
        }

        [TestMethod("IsEmailValid_EmailIsInvalid")]
        public void IsEmailValid_EmailIsInvalid_ReturnFalse()
        {
            var users = new List<User>() {
             new User()
            {
                Email = "@gmail.com"
            },
            new User()
            {
                Email = "randomgmailcom"
            },
             new User()
            {
                Email = "ra@ndom@gmailcom"
            },
            new User()
            {
                Email = "ra\"nd\"om@gmailcom"
            },
             new User()
            {
                Email = "ra\"nd\"om@gmailcom"
            },
              new User()
            {
                Email = "b(c)d,e:f;gi[jk]l@domain.com"
            }
            };

            users.ForEach(u =>
            {
                Assert.IsFalse(u.IsEmailValid());
            });
        }

        [TestMethod("IsEmailValid_EmailIsValid")]
        public void IsEmailValid_EmailIsValid_ReturnTrue()
        {
            IUser user = new User()
            {
                Email = Utility.RandomStringGenerator(3) + "@domain.net"
            };

            IUser user2 = new User()
            {
                Email = Utility.RandomStringGenerator(35) + "@" + Utility.RandomStringGenerator(4) + "." + Utility.RandomStringGenerator(2)
            };
            Assert.IsTrue(user.IsEmailValid() && user2.IsEmailValid());
        }

        ///
        /// TrimProperties
        ///

        [TestMethod("TrimProperties_EnterNotTrimmedProperties")]
        public void TrimProperties_EnterNotTrimmedProperties_ReturnTrimmedProp()
        {
            // arrange
            IUser user = new User();
            foreach (PropertyInfo p in user.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(string) && p.CanWrite)
                {
                    p.SetValue(user, " " + Utility.RandomStringGenerator(5) + " ");
                }
            }

            // act
            user.TrimProperties();

            // assert
            foreach (PropertyInfo p in user.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(string) && p.CanWrite)
                {
                    Assert.IsTrue(p.GetValue(user).ToString().Length == 5);
                }
            }
        }
    }
}