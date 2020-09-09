using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApi.BusinessLogic.Services.Internal.Tests
{
    [TestClass()]
    public class EmailServiceTests
    {

        private readonly EmailService _emailService = new EmailService(EmailType.BlueGray);

        #region SendConfirmEmailEmail

        
        [TestMethod()]
        public void SendConfirmEmailEmail_blueGrayType_returnValidEmail()
        {
            Assert.IsTrue(true); // TODO when implemented
        }

        #endregion
    }
}