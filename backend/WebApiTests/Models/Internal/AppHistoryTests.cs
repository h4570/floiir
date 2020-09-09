using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiTests;

namespace WebApi.Models.Internal.Tests
{
    [TestClass()]
    public class AppHistoryTests
    {

        [TestMethod()]
        public void AppHistoryLongDescriptionShouldBeTrimmedTest()
        {
            string desc = Utility.RandomStringGenerator(250);

            var obj = new AppHistory(AppTable.TableName, AppHistoryType.Add, 1, 1, desc);
            Assert.IsTrue(obj.Description.Length == 200);
        }

    }
}