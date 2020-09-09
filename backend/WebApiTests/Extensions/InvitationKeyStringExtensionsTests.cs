using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApi.Extensions.Tests
{
    [TestClass()]
    public class InvitationKeyStringExtensionsTests
    {
        #region IsInvKeyValid

        [TestMethod("IsInvKeyValid_StringTooLong")]
        public void IsInvKeyValid_StringTooLong_ReturnFalse()
        {
            string invKey = "12345678901";

            Assert.IsFalse(invKey.IsInvKeyValid());
        }

        [TestMethod("IsInvKeyValid_StringTooShort")]
        public void IsInvKeyValid_StringTooShort_ReturnFalse()
        {
            string invKey = "123456789";

            Assert.IsFalse(invKey.IsInvKeyValid());
        }

        [TestMethod("IsInvKeyValid_GoodLenghtString")]
        public void IsInvKeyValid_GoodLengthString_ReturnTrue()
        {
            string invKey = "1234567890";

            Assert.IsTrue(invKey.IsInvKeyValid());
        }

        #endregion
    }
}