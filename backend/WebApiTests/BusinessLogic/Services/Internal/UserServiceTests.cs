using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos.Internal;
using WebApiTests;
using WebApi.Models.Internal;
using System.Threading.Tasks;
using System;

namespace WebApi.BusinessLogic.Services.Internal.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private UserService _userService;
        public AppDbContext _context;
        private ConfigEnvironment _config;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new AppDbContext(options);
            _userService = new UserService(_context);
            _config = Utility.GetConfig();
        }

        #region ValidateBeforeRegister

        [TestMethod("ValidateBeforeRegister_passInvalidPayload")]
        public void ValidateBeforeRegister_passInvalidPayload_return463Code()
        {
            // arrange
            var invalidUsers = new List<UserDto>()
            {
                new UserDto()
                {
                    Login=null,
                    Password = Utility.RandomStringGenerator(6),
                    FirstName = Utility.RandomStringGenerator(3),
                    LastName = Utility.RandomStringGenerator(3),
                    Email = Utility.RandomStringGenerator(3) + "@domain.net",
                },
                 new UserDto()
                {
                    Login=Utility.RandomStringGenerator(20),
                    Password = null,
                    FirstName = Utility.RandomStringGenerator(3),
                    LastName = Utility.RandomStringGenerator(3),
                    Email = Utility.RandomStringGenerator(3) + "@domain.net",
                },
                  new UserDto()
                {
                    Login=Utility.RandomStringGenerator(20),
                    Password = Utility.RandomStringGenerator(6),
                    FirstName = null,
                    LastName = Utility.RandomStringGenerator(3),
                    Email = Utility.RandomStringGenerator(3) + "@domain.net",
                },
                   new UserDto()
                {
                    Login=Utility.RandomStringGenerator(20),
                    Password = Utility.RandomStringGenerator(6),
                    FirstName = Utility.RandomStringGenerator(3),
                    LastName = null,
                    Email = Utility.RandomStringGenerator(3) + "@domain.net",
                },
                    new UserDto()
                {
                    Login=Utility.RandomStringGenerator(20),
                    Password = Utility.RandomStringGenerator(6),
                    FirstName = Utility.RandomStringGenerator(3),
                    LastName = Utility.RandomStringGenerator(3),
                    Email = null,
                },
            };

            // act
            foreach (var invalidUser in invalidUsers)
            {
                var invalidRegister = _userService.ValidateBeforeRegister(invalidUser);
                Assert.AreEqual(invalidRegister.FailStatusCode, 463); //assert
            }
        }

        [TestMethod("ValidateBeforeRegister_passExistingEmail")]
        public void ValidateBeforeRegister_passExistingEmail_return464Code()
        {
            // assert
            var userDto = new UserDto()
            {
                Id = 1,
                Login = "app-admin",
                Password = "no-password",
                Email = "admin@flooir.com",
                FirstName = "Floiir",
                LastName = "Admin"
            };
            _context.Add(new User(userDto));
            _context.SaveChanges();

            // act
            var validation = _userService.ValidateBeforeRegister(userDto);

            // assert
            Assert.AreEqual(validation.FailStatusCode, 464);
        }

        [TestMethod("ValidateBeforeRegister_passExistingLogin")]
        public void ValidateBeforeRegister_passExistingLogin_return465Code()
        {
            // assert
            var userDto = new UserDto()
            {
                Id = 1,
                Login = "app-admin",
                Password = "no-password",
                Email = "admin@flooir.com",
                FirstName = "Floiir",
                LastName = "Admin"
            };
            _context.Add(new User(userDto));
            _context.SaveChanges();
            userDto.Email = "differentEmail@flooir.com";

            // act
            var validation = _userService.ValidateBeforeRegister(userDto);

            // assert
            Assert.AreEqual(validation.FailStatusCode, 465);
        }

        [TestMethod("ValidateBeforeRegister_passExistingLogin")]
        public void ValidateBeforeRegister_passValidUser_returnSuccess()
        {
            // assert
            var userDto = new UserDto()
            {
                Id = 1,
                Login = "app-admin",
                Password = "no-password",
                Email = "admin@flooir.com",
                FirstName = "Floiir",
                LastName = "Admin"
            };

            // act
            var validation = _userService.ValidateBeforeRegister(userDto);

            // assert
            Assert.IsTrue(validation.Succeed);
        }

        #endregion

        #region ComputePasswordHashAndAddUser

        [TestMethod("ComputePasswordHashAndAddUser_passValidUser")]
        public async Task ComputePasswordHashAndAddUser_passValidUser_returnSavedUserWithHashedPassword()
        {
            // assert
            var userDto = new UserDto()
            {
                Id = 1,
                Login = "app-admin",
                Password = "no-password",
                Email = "admin@flooir.com",
                FirstName = "Floiir",
                LastName = "Admin"
            };


            // act
            var addedUser = await _userService.ComputePasswordHashAndAddUser(new User(userDto), _config);

            // assert
            Assert.IsTrue(addedUser.Password != userDto.Password
                          && await _context.Users
                                   .AnyAsync(x => x.Id == addedUser.Id));
        }



        #endregion
    }
}