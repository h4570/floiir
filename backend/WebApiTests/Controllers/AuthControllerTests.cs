using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.Tests
{
    [TestClass()]
    public class AuthControllerTests
    {

        public AppDbContext _context;
        private AppHistoryController appHistoryController;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDb").Options;
            _context = new AppDbContext(options);
            appHistoryController = new AppHistoryController(options);
        }

        [TestMethod()]
        public void AuthorizeTest()
        {
            Assert.IsTrue(true);
        }
    }
}