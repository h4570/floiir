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
        public void AppHistoryLongDescriptionShouldBeTrimmedTest()
        {
            var desc = new StringBuilder();
            for (var i = 0; i < 250; i++)
                desc.Append(i);
            var obj = new AppHistory(AppTable.TableName, AppHistoryType.Add, 1, "1", 1, desc.ToString());
            Assert.IsTrue(obj.Description.Length == 199);
        }

    }
}