using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebApiTests;


namespace WebApi.BusinessLogic.Services.External.Tests
{
  
    [TestClass()]
    public class ReCaptchaServiceTests
    {
        private ReCaptchaService reCaptchaService = new ReCaptchaService();
        private ConfigEnvironment config = Utility.GetConfig();

        #region IsReCaptchaSucceed

        [TestMethod()]
        public async Task IsReCaptchaSucceed_passDevCapcha_returnsTrue()
        {

            var result = await reCaptchaService.IsReCaptchaSucceed(
                               "test",
                               config.ReCaptchaSecret,
                               "localhost");

            Assert.IsTrue(result);
        }

        #endregion
    }
}