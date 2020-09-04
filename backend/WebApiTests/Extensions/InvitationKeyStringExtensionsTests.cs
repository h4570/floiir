using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Extensions.Tests
{
    [TestClass()]
    public class InvitationKeyStringExtensionsTests
    {
        [TestMethod()]
        public void IsInvKeyValid_StringTooLong_ReturnFalse()
        {
            string invKey = "12345678901";

            Assert.IsFalse(invKey.IsInvKeyValid());
        }

        [TestMethod()]
        public void IsInvKeyValid_StringTooShot_ReturnFalse()
        {
            string invKey = "123456789";

            Assert.IsFalse(invKey.IsInvKeyValid());
        }

        [TestMethod()]
        public void IsInvKeyValid_GoodLenghtString_ReturnTrue()
        {
            string invKey = "1234567890";

            Assert.IsTrue(invKey.IsInvKeyValid());
        }
    }
}