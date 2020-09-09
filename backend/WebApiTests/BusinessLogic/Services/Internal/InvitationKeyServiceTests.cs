using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Models.Internal;
using System.Threading.Tasks;

namespace WebApi.BusinessLogic.Services.Internal.Tests
{
    [TestClass()]
    public class InvitationKeyServiceTests
    {
        private  InvitationKeyService _invitationKeyService;
        public AppDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new AppDbContext(options);
            _invitationKeyService = new InvitationKeyService(_context);
        }

        #region ValidateAndReturnObject

        [TestMethod("ValidateAndReturnObject_passedValidKey")]
        public async Task ValidateAndReturnObject_passedValidKey_returnTrue()
        {
            // arrange
            var invKeyDate = new DateTime(2005, 4, 2, 21, 37, 00);
            var firstInvKey = new InvitationKey
            {
                Id = Constants.FIRST_INV_KEY_ID,
                InviterId = Constants.APP_ADMIN_ID,
                CreatedAt = invKeyDate,
                Key = "1234567890",
            };
            _context.InvitationKeys.Add(firstInvKey);
            _context.SaveChanges();

            // act
            var keyCheck =await _invitationKeyService.ValidateAndReturnObject("1234567890");

            // assert
            Assert.IsTrue(keyCheck.Succeed);
        }

        [TestMethod("ValidateAndReturnObject_passedUsedKey")]
        public async Task ValidateAndReturnObject_passedUsedKey_return462Code()
        {
            // arrange
            var invKeyDate = new DateTime(2005, 4, 2, 21, 37, 00);
            var firstInvKey = new InvitationKey
            {
                Id = Constants.FIRST_INV_KEY_ID,
                InviterId = Constants.APP_ADMIN_ID,
                CreatedAt = invKeyDate,
                Key = "1234567890",
                UsedByUserId=1
            };
            _context.InvitationKeys.Add(firstInvKey);
            _context.SaveChanges();

            // act
            var keyCheck = await _invitationKeyService.ValidateAndReturnObject("1234567890");

            // assert
            Assert.IsTrue(keyCheck.FailStatusCode==462);
        }

        [TestMethod("ValidateAndReturnObject_passedNotExistingKey")]
        public async Task ValidateAndReturnObject_passedNotExistingKey_return461Code()
        {
            // arrange

            // act
            var keyCheck = await _invitationKeyService.ValidateAndReturnObject("1234567890");

            // assert
            Assert.IsTrue(keyCheck.FailStatusCode == 461);
        }

        [TestMethod("ValidateAndReturnObject_passedWorngLengthKey")]
        public async Task ValidateAndReturnObject_passedWorngLengthKey_return460Code()
        {
            // arrange

            // act
            var keyCheck = await _invitationKeyService.ValidateAndReturnObject("123456789");
            var keyCheck2 = await _invitationKeyService.ValidateAndReturnObject("12345678901");

            // assert
            Assert.IsTrue(keyCheck.FailStatusCode == 460 && keyCheck2.FailStatusCode==460);
        }

        #endregion

        #region SetInvitationKeyAsUsed

        [TestMethod("SetInvitationKeyAsUsed_passedValidKey")]
        public async Task SetInvitationKeyAsUsed_passedValidKey_returnTrue()
        {
            // arrange
            var invKeyDate = new DateTime(2005, 4, 2, 21, 37, 00);
            var firstInvKey = new InvitationKey
            {
                Id = Constants.FIRST_INV_KEY_ID,
                InviterId = Constants.APP_ADMIN_ID,
                CreatedAt = invKeyDate,
                Key = "1234567891"
            };
            _context.InvitationKeys.Add(firstInvKey);
            _context.SaveChanges();

            // act
            await _invitationKeyService.SetInvitationKeyAsUsed("1234567891", new User() { Id = 2 });

            // assert
            Assert.IsTrue(await _context.InvitationKeys
                                .AnyAsync(x => x.UsedByUserId == 2));
        }


        [TestMethod("SetInvitationKeyAsUsed_passedUsedKey")]
        public async Task SetInvitationKeyAsUsed_passedUsedKey_returnTrue()
        {
            // arrange
            var invKeyDate = new DateTime(2005, 4, 2, 21, 37, 00);
            var firstInvKey = new InvitationKey
            {
                Id = Constants.FIRST_INV_KEY_ID,
                InviterId = Constants.APP_ADMIN_ID,
                CreatedAt = invKeyDate,
                Key = "1234567890",
                UsedByUserId = 1
            };
            _context.InvitationKeys.Add(firstInvKey);
            _context.SaveChanges();

            // act
            await _invitationKeyService.SetInvitationKeyAsUsed("1234567890",new User() { Id=2});

            // assert
            Assert.IsTrue(await _context.InvitationKeys
                                .AnyAsync(x=>x.UsedByUserId==2));
        }

        #endregion
    }
}