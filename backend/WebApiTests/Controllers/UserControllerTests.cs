using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiTests;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        public AppDbContext _context;
        private UserController userController;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDb").Options;
            _context = new AppDbContext(options);
            userController = new UserController(options, Options.Create(Utility.GetConfig()));
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }
    }
}