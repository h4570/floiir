using WebApi.Models.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Models.Internal.Tests
{
    [TestClass()]
    public class AppHistoryTests
    {

        [TestMethod()]
        public void AppHistoryTest1()
        {
            var desc = "X5cR3OY6AOPAIgxnRq1EeaAliIIs2giXkCJFKLBuPZI6CBSwKNLLKpePACWBd0efXnEnnpzCuBvuKg7EYpuvvHxRWnqTlDsIujAsdbFa5Dj1f6nZgObEc4ujP5Os6mCowJGjzuX3aN43bK5P31Wy9C18TsXPKOWXdB5KQuWYOsWZRsm9pujlikzHJA2ihvAJlQCtCqcatMF27GJBOP";
            var obj = new AppHistory(AppTable.TableName, AppHistoryType.Add, 1, "1", 1, desc);
            Assert.IsTrue(obj.Description.Length == 199);
        }

    }
}